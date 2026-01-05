using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;
using System.Drawing;

namespace server_app.games
{
	public struct gameStats
	{
		public List<bool> correct;
		public List<double> accuracy;
		public List<TimeSpan> time;

		public gameStats()
		{
			correct = [];
			accuracy = [];
			time = [];
		}
		public readonly void Update(double accuracy, TimeSpan time, bool correct)
		{
			this.accuracy.Add(accuracy);
			this.correct.Add(correct);
			this.time.Add(time);
		}
	}

	public enum Games
	{
		Accuracy,
		Versus,
		Elimination,
	}
	public interface IPlayable
	{
		bool QueueUser(string userID);
		bool DequeueUser(string userID);
		Task UpdateUsers();
		Task StartGame();
		Task SubmissionPhase();
		void LoadResponse(string userID, byte[] input);
		void EvaluationPhase(char letter);
		Task ContinueRequest(string userID);
		void EndGame();
		Games GetGameType();
		string GetGameID();
		bool HasStarted();
		int GetPlayerCount();
		int GetMaxPlayers();
	}

	public abstract class Game
	{
		protected IHubContext<Connection> _hubContext;

		protected string _gameID;
		protected int _maxPlayers;
		protected Games _type;
		private bool _started;

		protected List<string> userIDs;
		protected List<friendData> _userDatas;
		protected Dictionary<string, gameStats> _gameStats;

		protected Random _rnd;
		protected List<char> _letters;
		protected int _roundCount;

		protected DateTime _startTime;
		protected Dictionary<string, (double[] submission, DateTime time)> _currentResponses;
		protected List<string> _continueRequests;

		public Games GetGameType() => _type;
		public string GetGameID() => _gameID;
		public bool HasStarted() => _started;
		public int GetMaxPlayers() => _maxPlayers;
		public int GetPlayerCount() => userIDs.Count;

		public Game(IHubContext<Connection> context, Games type, string userID, int maxPlayers)
		{
			_hubContext = context;

			_gameID = userID + DateTime.UtcNow.ToString();
			this._maxPlayers = maxPlayers;
			this._type = type;
			_started = false;

			userIDs = [];
			_userDatas = [];
			_gameStats = [];

			_rnd = new();
			_letters = [];
			_roundCount = 0;

			_currentResponses = [];
			_continueRequests = [];
		}

		public bool QueueUser(string userID)
		{
			if (!Database.LoadFriendData(userID, out friendData data))
			{
				return false;
			}

			userIDs.Add(userID);
			_userDatas.Add(data);
			return true;
		}
		public virtual bool DequeueUser(string userID)
		{
			int index = -1;
			for (int i = 0; i < GetPlayerCount(); i++)
			{
				if (_userDatas[i].userID == userID)
				{
					index = i;
					break;
				}
			}

			if (index == -1)
				return false;

			userIDs.Remove(userID);
			_userDatas.RemoveAt(index);

			return true;
		}
		public async Task UpdateUsers()
		{
			foreach (var user in userIDs)
			{
				if (Connection.map.TryGetValue(user, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("updateUsers", _userDatas);
				}
			}
		}

		public virtual async Task StartGame()
		{
			_started = true;

			foreach (string user in userIDs)
			{
				_gameStats.Add(user, new gameStats());
			}

			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("awaitStart");
				}
			}

			await Task.Delay(5000);

			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("startGame");
				}
			}
		}

		protected List<char> GenerateLetters(int count)
		{
			List<char> letters = [];

			Random rnd = new();
			for (int i = 0; i < count; i++)
			{
				letters.Add((char)(rnd.Next(0, 26) + 65));
			}
			return letters;
		}
		protected virtual async Task AwaitRound()
		{
			_continueRequests.Clear();
			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("awaitRound");
				}
			}
		}
		protected async Task SendLetter(List<string> userIDs, char letter)
		{
			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("receiveLetter", letter);
				}
			}
		}

		public virtual void LoadResponse(string userID, byte[] input)
		{
			DateTime endTime = DateTime.UtcNow;
			double[] array;

			using (var ms = new MemoryStream(input))
			{
				var bmp = new Bitmap(ms);
				array = Data.PreprocessImage(bmp);
			}

			_currentResponses.Add(userID, (array, endTime));
		}
		protected bool EvaluateSubmission(string userID, char character)
		{
			int letter = character - 65;

			Network network = new(_currentResponses[userID].submission);
			bool correct = network.GetResult() == letter;

			if (_gameStats.TryGetValue(userID, out gameStats currentStats))
			{
				DateTime endTime = _currentResponses[userID].time;
				double accuracy = network.GetAccuracy(letter);

				currentStats.Update(accuracy, endTime - _startTime, correct);
			}
			_gameStats[userID] = currentStats;
			return correct;
		}
		protected async Task SendResult(string userID, gameStats stats)
		{
			bool correct = stats.correct[_roundCount];
			double accuracy = stats.accuracy[_roundCount];
			TimeSpan time = stats.time[_roundCount];

			if (Connection.map.TryGetValue(userID, out string? connectionID))
			{
				await _hubContext.Clients.Client(connectionID).SendAsync("receiveResults", correct, accuracy, time);
			}
		}

		public virtual async void EndGame()
		{
			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("endGame");
				}
			}

			foreach (string userID in userIDs)
			{
				for (int i = 0; i < _gameStats[userID].accuracy.Count; i++)
				{
					// iterate through each round the current user completed - in Elimination each user may complete a different number of rounds
					if (Database.LoadUserData(userID, out userData userData))
					{
						// reload userData after each update for duplicate letters
						await UpdateStatistics(userData, _letters[i], i);
					}
					else
					{
						Database.outputException("Failed to retrieve statistics");
					}
				}
			}
		}
		private async Task UpdateStatistics(userData userData, char letter, int round)
		{
			double accuracy = userData.statistics[letter].accuracy;
			TimeSpan time = userData.statistics[letter].time;
			int total = userData.statistics[letter].total;

			double updatedAccuracy = (accuracy * total + _gameStats[userData.userID].accuracy[round]) / (total + 1);
			TimeSpan updatedTime = (time * total + _gameStats[userData.userID].time[round]) / (total + 1);

			if (!Database.UpdateStatistics(userData.userID, letter, updatedAccuracy, updatedTime, total + 1))
			{
				Database.outputException("Failed to update statistics");
				return;
			}

			if (Connection.map.TryGetValue(userData.userID, out string? connectionID))
			{
				// update statistics client-side for the indexed round
				statistics updated = new(updatedAccuracy, updatedTime, total + 1);
				await _hubContext.Clients.Client(connectionID).SendAsync("updateStatistics", letter, updated);
			}
		}
	}
}
