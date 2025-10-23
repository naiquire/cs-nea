using Microsoft.Data.Sqlite;

namespace server_app.databases
{
	/* the todo list of DOOM
	 * 
	 * home -> panel_main ui
	 * client error handling on server crash
	 * TRANSLATIONS
	 * language not update when changed inside profile
	 * can place cursor in home/aboutMe and other places
	 * btn_home ui
	 * game -> "next letter in" dissapears ONLY on round 1
	 * game -> panel_drawing sometimes invisible until click
	 * game -> round results screen especially v,k
	 * endGame ui
	 * profile -> about me
	 * cleanup ui code
	 * ensure ALL server errors are handled by client in some way
	 */

	

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
		public string aboutMe { get; set; }
		public bool online { get; set; }

		public string localisation { get; set; }
		public DateTime dateCreated { get; set; }

		public int rank { get; set; }
		public Dictionary<char, statistics> statistics { get; set; }
	}
	public static class @database
	{
		private static readonly string dbPath = $@"Data Source={Environment.GetEnvironmentVariable("cs-nea-server") ?? @"H:\CompSci\cs-nea\server-app\server-app"}\databases\maindb.sqlite";
		private static readonly SqliteConnection connection = new(dbPath);
		public static void outputException(Exception ex)
		{
			// if exception occurs then log the message and allow the client to try again
			Logger.ErrorLog(ex);
		}
		public static void outputException(string ex)
		{
			// if exception occurs then log the message and allow the client to try again
			Logger.ErrorLog(ex);
		}

		public static bool loginRequest(string userID, string password, out int success)
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
		public static bool accountRequest(string userID, string password, string localisation, out int success)
		{
			if (userExists(userID, out bool exists))
			{
				if (!exists)
				{
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
				else
				{
					success = 0;
					return true;
				}
			}
			else
			{
				success = -1;
				return false;
			}
		}
		public static bool userExists(string userID, out bool exists)
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
		public static bool loadUserData(string userID, out userData userData)
		{
			userData = new();

			if (!userExists(userID, out bool exists))
			{
				return false;
			}

			if (!exists)
			{
				return true; // return empty userData
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

			if (loadStatistics(userID, out var stats))
			{
				userData.statistics = stats;
			}
			else
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

			userData.userID = userID;
			userData.friends = friendData;

			return true;
		}
		public static bool updateUserData(string userID, string aboutMe, string localisation)
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
		public static bool loadStatistics(string userID, out Dictionary<char, statistics> statistics)
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
		public static bool loadFriends(string userID, out List<string> friends)
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
		public static bool loadFriendData(string userID, out friendData friendData)
		{
			friendData = new();
			string query = @"SELECT aboutMe, dateCreated, localisation, rank
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
						friendData.aboutMe = reader.GetString(0);
						friendData.dateCreated = reader.GetDateTime(1);
						friendData.localisation = reader.GetString(2);
						friendData.rank = reader.GetInt32(3);
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

			if (loadStatistics(userID, out var stats))
			{
				friendData.statistics = stats;
			}
			else
			{
				return false;
			}

			friendData.userID = userID;
			friendData.online = connections.connection.map.ContainsKey(userID);
			return true;
		}
		public static bool addFriends(string user1, string user2)
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
		public static bool removeFriends(string user1, string user2)
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
		public static bool saveInvite(string userID, string senderID)
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
		public static bool loadInvites(string userID, out List<string> senderIDs)
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

			bool success = deleteInvites(userID);
			return success;
		}
		private static bool deleteInvites(string userID)
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
		public static bool updateStatistics(string userID, char letter, double accuracy, TimeSpan time, int total)
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
		public static bool updateRank(string userID, int rank)
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
