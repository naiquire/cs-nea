using Microsoft.Data.Sqlite;

namespace server_app.databases
{
	public struct userData
	{
		public string userID { get; set; }
		public string aboutMe { get; set; }
		public List<friendData> friends { get; set; }
		public string localisation { get; set; }
		public DateTime dateCreated { get; set; }
		public int rank { get; set; }
		public Dictionary<char, statistics> statistics { get; set; }
	}
	public struct @statistics(double a, TimeSpan ts, int t)
	{
		public double accuracy { get; set; } = a;
		public TimeSpan time { get; set; } = ts;
		public int total { get; set; } = t;
	}
	public struct friendData
	{
		public string userID { get; set; }
		public bool online { get; set; }
		public int rank { get; set; }
	}
	public static class Database
	{
		private static readonly string dbPath = $@"Data Source={Environment.GetEnvironmentVariable("cs-nea-server")}\databases\maindb.sqlite";
		private static readonly SqliteConnection connection = new(dbPath);
		public static void outputException(Exception ex) => Logger.ErrorLog(ex.Message);
		public static void outputException(string ex) => Logger.ErrorLog(ex);

		public static bool LoginRequest(string userID, string password, out int success)
		{
			string query = @"SELECT userData.password
				FROM userData
				WHERE userData.userID = @userID";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);

					using (var reader = command.ExecuteReader())
					{
						success = 2;

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
			catch (Exception ex)
			{
				outputException(ex);
				success = -1;
				connection.Close();
				return false;
			}
		}
		public static bool AccountRequest(string userID, string password, string localisation, out int success)
		{
			if (!UserExists(userID, out bool exists))
			{
				success = -1;
				return false;
			}
			if (exists)
			{
				success = 0;
				return true;
			}

			string query = @"INSERT INTO userData
						VALUES (@userID, @password, @aboutMe, @dateCreated, @rank, @localisation)";
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
						command.Parameters.AddWithValue("@letter", ((char)(i + 65)).ToString());
						command.Parameters.AddWithValue("@accuracy", 0);
						command.Parameters.AddWithValue("@time", TimeSpan.Zero);
						command.Parameters.AddWithValue("@total", 0);

						command.ExecuteNonQuery();
					}
				}

				connection.Close();
				success = 1;
				return true;
			}
			catch (Exception ex)
			{
				outputException(ex);
				success = -1;
				connection.Close();
				return false;
			}

		}
		private static bool UserExists(string userID, out bool exists)
		{
			string query = @"SELECT userID
				FROM userData
				WHERE userID = @userID";
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
			catch (Exception ex)
			{
				outputException(ex);
				exists = false;
				connection.Close();
				return false;
			}
		}
		public static bool LoadUserData(string userID, out userData userData)
		{
			userData = new();

			if (!UserExists(userID, out bool exists))
			{
				// error checking if user exists
				return false;
			}

			if (!exists)
			{
				// return empty userData
				return true;
			}

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
						userData.dateCreated = reader.GetDateTime(1);
						userData.rank = reader.GetInt32(2);
						userData.localisation = reader.GetString(3);
					}
				}
				connection.Close();
			}
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}

			if (!LoadStatistics(userID, out var stats))
			{
				// error loading statistics
				return false;
			}

			if (!LoadFriends(userID, out List<string> friends))
			{
				// error loading friends
				return false;
			}

			List<friendData> friendData = [];
			foreach (var friend in friends)
			{
				if (!LoadFriendData(friend, out friendData data))
				{
					// error loading friend data
					return false;
				}
				friendData.Add(data);
			}

			userData.userID = userID;
			userData.friends = friendData;
			userData.statistics = stats;

			return true;
		}
		public static bool UpdateUserData(string userID, string aboutMe, string localisation)
		{
			string query = @"UPDATE userData
				SET aboutMe = @aboutMe, localisation = @localisation
				WHERE userID = @userID";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);
					command.Parameters.AddWithValue("@aboutMe", aboutMe);
					command.Parameters.AddWithValue("@localisation", localisation);

					command.ExecuteNonQuery();
				}
				connection.Close();
				return true;
			}
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}
		}
		private static bool LoadStatistics(string userID, out Dictionary<char, statistics> statistics)
		{
			statistics = [];
			string query = @"SELECT letter, accuracy, time, total
				FROM statistics
				WHERE userID = @userID";
			try
			{
				connection.Open();
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

						statistics[letter] = new statistics(accuracy, time, total);
					}
				}
				connection.Close();
				return true;
			}
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}
		}
		public static bool LoadFriends(string userID, out List<string> friends)
		{
			friends = [];
			string query = @"SELECT user1, user2
				FROM friends
				WHERE user1 = @userID OR user2 = @userID";
			try
			{
				connection.Open();
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
				connection.Close();
				return true;
			}
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}
		}
		public static bool LoadFriendData(string userID, out friendData friendData)
		{
			friendData = new();
			string query = @"SELECT rank
				FROM userData
				WHERE userID = @userID";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						friendData.rank = reader.GetInt32(0);
					}
				}
				connection.Close();
			}
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}

			friendData.userID = userID;
			friendData.online = connections.Connection.map.ContainsKey(userID);
			return true;
		}
		public static bool AddFriends(string user1, string user2)
		{
			string query = @"INSERT INTO friends
				VALUES (@user1, @user2)";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@user1", user1);
					command.Parameters.AddWithValue("@user2", user2);

					command.ExecuteNonQuery();
				}
				connection.Close();
				return true;
			}
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}
		}
		public static bool RemoveFriends(string user1, string user2)
		{
			string query = @"DELETE FROM friends
				WHERE (user1 = @user1 AND user2 = @user2) OR (user2 = @user1 AND user1 = @user2)";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@user1", user1);
					command.Parameters.AddWithValue("@user2", user2);

					command.ExecuteNonQuery();
				}
				connection.Close();
				return true;
			}
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}
		}
		public static bool SaveInvite(string userID, string senderID)
		{
			string query = @"INSERT INTO friendInvites
				VALUES (@userID, @senderID)";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);
					command.Parameters.AddWithValue("@senderID", senderID);

					command.ExecuteNonQuery();
				}
				connection.Close();
				return true;
			}
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}
		}
		public static bool LoadInvites(string userID, out List<string> senderIDs)
		{
			senderIDs = [];
			string query = @"SELECT senderID
				FROM friendInvites
				WHERE userID = @userID";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						string sender = reader.GetString(0);
						senderIDs.Add(sender);
					}
				}
				connection.Close();
			}
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}

			bool success = DeleteInvites(userID);
			return success;
		}
		private static bool DeleteInvites(string userID)
		{
			string query = @"DELETE	FROM friendInvites
				WHERE userID = @userID";
			try
			{
				connection.Open();
				using (var command = new SqliteCommand(query, connection))
				{
					command.Parameters.AddWithValue("@userID", userID);
					command.ExecuteNonQuery();
				}
				connection.Close();
				return true;
			}
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}
		}
		public static bool UpdateStatistics(string userID, char letter, double accuracy, TimeSpan time, int total)
		{
			string query = @"UPDATE statistics
				SET accuracy = @accuracy, time = @time, total = @total
				WHERE userID = @userID AND letter = @letter";
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
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}
		}
		public static bool UpdateRank(string userID, int rank)
		{
			string query = @"UPDATE userData
				SET rank = @rank
				WHERE userID = @userID";
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
			catch (Exception ex)
			{
				outputException(ex);
				connection.Close();
				return false;
			}
		}
	}
}
