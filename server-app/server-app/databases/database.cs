using MathNet.Numerics.Statistics;
using Microsoft.Data.Sqlite;
using server_app.connections;
using server_app.games;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace server_app.databases
{
	// handles all requests to the SQL database
	public struct userData
	{
		public string userID;
		public string aboutMe;
		public List<friendData> friends;

		public string localisation;
		public DateTime dateCreated;

		public int rank;
		public Dictionary<char, (double accuracy, TimeSpan time, int total)> statistics;
	}
	public struct friendData
	{
		public string userID;
		public string aboutMe;
		public bool online;

		public string localisation;
		public DateTime dateCreated;

		public int rank;
		public Dictionary<char, (double accuracy, TimeSpan time, int total)> statistics;
	}
	public static class @database
	{
		private static readonly string dbPath = $@"Data Source={Environment.GetEnvironmentVariable("cs-nea-server")}\databases\maindb.sqlite";
		private static readonly SqliteConnection connection = new(dbPath);
		public static void outputException(Exception ex)
		{
			// if exception occurs then log the message and allow the client to try again
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"[ERROR] {ex}");
			Console.ResetColor();
		}
		public static void outputException(string ex)
		{
			// if exception occurs then log the message and allow the client to try again
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"[ERROR] {ex}");
			Console.ResetColor();
		}

		public static bool loginRequest(string userID, string password, out int success)
		{
			string query = "SELECT userData.password FROM userData WHERE userData.userID = @userID";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);

					using (var reader = command.ExecuteReader())
					{
						success = -1;

						while (reader.Read())
						{
							if (reader.GetString(0) == password)
							{
								success = 1;
							}
							else
							{
								success = 0;
							}
						}
					}
				}
				connection.Close();
				return true;
			}
			catch (SqliteException ex)
			{
				outputException(ex);
				success = -1;
				return false;
			}
		}
		public static bool accountRequest(string userID, string password, string localisation, out int success)
		{
			if (userExists(userID, out bool exists))
			{
				if (!exists)
				{
					string query = @"INSERT INTO userData
							VALUES(@userID, @password, @aboutMe, @dateCreated, @rank, @localisation)";
					try
					{
						connection.Open();
						using (var command = new SqliteCommand(query, connection))
						{
							command.Parameters.AddWithValue("@userID", userID);
							command.Parameters.AddWithValue("@password", password);
							command.Parameters.AddWithValue("@aboutMe", "");
							command.Parameters.AddWithValue("@dateCreated", DateTime.UtcNow);
							command.Parameters.AddWithValue("@rank", 400);
							command.Parameters.AddWithValue("@localisation", localisation);

							command.ExecuteNonQuery();
						}

						for (int i = 0; i < 26; i++)
						{
							query = $@"INSERT INTO statistics
								VALUES(@userID, @letter, @accuracy, @time, @total)";

							using (var command = new SqliteCommand(query, connection))
							{
								command.Parameters.AddWithValue("@userID", userID);
								command.Parameters.AddWithValue("@letter", ((char)i + 65).ToString());
								command.Parameters.AddWithValue("@accuracy", 0);
								command.Parameters.AddWithValue("@time", TimeSpan.Zero);
								command.Parameters.AddWithValue("@total", 0);

								command.ExecuteNonQuery();
							}
						}

						connection.Close();
						success = 1;
					}
					catch (SqliteException ex)
					{
						outputException(ex);
						success = -1;
						return false;
					}
				}
				else
				{
					success = 0;
				}
			}
			else
			{
				success = -1;
			}
			return true;
		}
		public static bool userExists(string userID, out bool exists)
		{
			string query = "SELECT userID FROM userData WHERE userID = @userID";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);

					var reader = command.ExecuteReader();

					exists = reader.HasRows;
				}
				connection.Close();
				return true;
			}
			catch (SqliteException ex)
			{
				outputException(ex);
				exists = false;
				return false;
			}
		}
		public static bool loadUserData(string userID, out userData userData)
		{
			userData = new();

			string query = @"SELECT aboutMe, dateCreated, rank, localisation
				FROM userData
				WHERE userData.userID = @userID";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						userData.aboutMe = reader.GetString(0);
						userData.dateCreated = reader.GetDateTime(2);
						userData.rank = reader.GetInt32(3);
						userData.localisation = reader.GetString(4);
					}
				}
				connection.Close();
			}
			catch (SqliteException ex)
			{
				outputException(ex);
				return false;
			}

			userData.userID = userID;

			if (!loadStatistics(userID, out userData.statistics))
			{
				return false;
			}

			List<friendData> friendData = [];
			if (loadFriends(userID, out List<string> friends))
			{
				foreach (var friend in friends)
				{
					if (loadFriendData(friend, out friendData data))
					{
						friendData.Add(data);
					}
					else
					{
						return false;
					}
				}
			}

			return true;
		}
		public static bool loadStatistics(string userID, out Dictionary<char, (double, TimeSpan, int)> statistics)
		{
			statistics = [];
			string query = @"SELECT letter, accuracy, time, total
				FROM statistics
				WHERE userID = @userID";
			try
			{
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						char letter = reader.GetChar(0);
						double accuracy = reader.GetDouble(1);
						TimeSpan time = reader.GetTimeSpan(2);
						int total = reader.GetInt32(3);

						statistics[letter] = (accuracy, time, total);
					}
				}
				return true;
			}
			catch (SqliteException ex)
			{
				outputException(ex);
				return false;
			}
		}
		public static bool loadFriends(string userID, out List<string> friends)
		{
			friends = [];
			string query = @"SELECT user1, user2
				FROM friends
				WHERE user1 = @userID OR user2 = @userID";
			try
			{
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						string u1 = reader.GetString(0);
						string u2 = reader.GetString(1);

						if (u1 == userID)
						{
							friends.Add(u2);
						}
						if (u2 == userID)
						{
							friends.Add(u1);
						}
					}
				}
				return true;
			}
			catch (SqliteException ex)
			{
				outputException(ex);
				return false;
			}
		}
		public static bool loadFriendData(string userID, out friendData friendData)
		{
			friendData = new();
			string query = @"SELECT aboutMe, dateCreated, localisation, rank
				FROM userData
				WHERE userID = @userID";
			try
			{
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						friendData.aboutMe = reader.GetString(0);
						friendData.dateCreated = reader.GetDateTime(1);
						friendData.localisation = reader.GetString(2);
						friendData.rank = reader.GetInt32(3);
					}
				}
			}
			catch (SqliteException ex)
			{
				outputException(ex);
				return false;
			}

			if (!loadStatistics(userID, out friendData.statistics))
			{
				return false;
			}

			friendData.online = connections.connection.map.ContainsKey(userID);
			return true;
		}
		public static bool updateStatistics(string userID, char letter, double accuracy, TimeSpan time, int total)
		{
			string query = @"UPDATE statistics
				SET (statistics.accuracy = @accuracy, statistics.time = @time, statistics.total = @total)
				WHERE statistics.userID = @userID AND statistics.letter = @letter";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);
					command.Parameters.AddWithValue("@letter", letter);
					command.Parameters.AddWithValue("@accuracy", accuracy);
					command.Parameters.AddWithValue("@time", time);
					command.Parameters.AddWithValue("@total", total);

					command.ExecuteNonQuery();
				}
				connection.Close();
				return true;
			}
			catch (SqliteException ex)
			{
				outputException(ex);
				return false;
			}
		}
		public static bool updateRank(string userID, int rank)
		{
			string query = @"UPDATE userData
				SET (userData.rank = rank)
				WHERE userData.userID = @userID";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);
					command.Parameters.AddWithValue("@rank", rank);

					command.ExecuteNonQuery();
				}
				connection.Close();
				return true;
			}
			catch (SqliteException ex)
			{
				outputException(ex);
				return false;
			}
		}
	}
}
