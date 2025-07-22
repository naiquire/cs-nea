using Microsoft.Data.Sqlite;
using server_app.games;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace server_app.databases
{
	// handles all requests to the SQL database
	public struct userData
	{
		public string userID;
		public Dictionary<char, (double accuracy, TimeSpan time, int total)> statistics;
		public string aboutMe;
		public DateTime dateCreated;
		public int rank;
		public string localisation;
	}
	public static class @database
	{
		private static readonly string dbPath = @"Data Source=C:\Users\boyss\Documents\General\Relay\github\cs-nea-app\server-app\server-app\databases\maindb.sqlite";
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

					var reader = command.ExecuteReader();
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
					// account does not exist
					success = -1;
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
						connection.Close();

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

					if (reader.HasRows)
					{
						exists = true;
					}
					exists = false;
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
			Dictionary<char, (double, TimeSpan, int)> statistics = [];

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

			query = "SELECT letter, accuracy, time, total FROM statistics WHERE userID = @userID";
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
			}
			catch (SqliteException ex)
			{
				outputException(ex);
				return false;
			}

			userData.userID = userID;
			userData.statistics = statistics;
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
