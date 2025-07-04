using Microsoft.AspNetCore.SignalR;
using server_app.games;
using System.Reflection;

namespace server_app.connections
{
    public partial class @connection : Hub
    {
        /// <summary>
        /// Sends a confirmation to the user that a game has been successfully joined.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="gameID"></param>
        /// <returns></returns>
        /// <exception cref="DisconnectException"></exception>
        public async Task sendJoinConfirm(string userID, string gameID)
        {
            if (map.TryGetValue(userID, out string? connectionID))
            {
                await Clients.Client(connectionID).SendAsync("receiveJoinConfirm", gameID);
            }
            else
            {
                throw new DisconnectException(userID);
            }
        }

        /// <summary>
        /// Requests the clients to start their games.
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns></returns>
        /// <exception cref="DisconnectException"></exception>
        public async Task sendStartRequest(List<string> userIDs)
        {
            foreach (string userID in userIDs)
            {
                if (map.TryGetValue(userID, out string? connectionID))
                {
                    await Clients.Client(connectionID).SendAsync("startGame", userIDs);
                }
                else
                {
                    throw new DisconnectException(userID);
                }
            }
        }

        /// <summary>
        /// Sends a character to the given users.
        /// </summary>
        /// <param name="userIDs"></param>
        /// <param name="letter"></param>
        /// <returns></returns>
        /// <exception cref="DisconnectException"></exception>
        public async Task sendLetter(List<string> userIDs, char letter)
        {
            foreach (string userID in userIDs)
            {
                if (map.TryGetValue(userID, out string? connectionID))
                {
                    await Clients.Client(connectionID).SendAsync("receiveLetter", letter);
                }
                else
                {
                    throw new DisconnectException(userID);
                }
            }
        }

        /// <summary>
        /// Receives a submission from a user.
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="userID"></param>
        /// <param name="input"></param>
        public void receiveSubmission(string gameID, string userID, double[] input)
        {
            queueing.loadSubmission(gameID, userID, input);
        }

        /// <summary>
        /// Sends a user their result for the current character.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="stats"></param>
        /// <returns></returns>
        /// <exception cref="DisconnectException"></exception>
        public async Task sendResult(string userID, stats stats)
        {
            if (map.TryGetValue(userID, out string? connectionID))
            {
                await Clients.Client(connectionID).SendAsync("receiveResults", stats);
            }
            else
            {
                throw new DisconnectException(userID);
            }
        }

        /// <summary>
        /// Sends the unique results to users for the 1v1 game type.
        /// </summary>
        /// <param name="userIDs"></param>
        /// <param name="winner"></param>
        /// <returns></returns>
        /// <exception cref="DisconnectException"></exception>
        public async Task send1v1Results(List<string> userIDs, string? winner)
        {
            foreach (string userID in userIDs)
            {
                if (map.TryGetValue(userID, out string? connectionID))
                {
                    await Clients.Client(connectionID).SendAsync("receive1v1Result", winner);
                }
                else
                {
                    throw new DisconnectException(userID);
                }
            }
        }

        /// <summary>
        /// Sends the unique results to users for the knockout game type.
        /// </summary>
        /// <param name="userIDs"></param>
        /// <param name="aliveUsers"></param>
        /// <returns></returns>
        /// <exception cref="DisconnectException"></exception>
        public async Task sendKnockoutResults(List<string> userIDs, List<string> aliveUsers)
        {
            foreach (string userID in userIDs)
            {
                if (map.TryGetValue(userID, out string? connectionID))
                {
                    await Clients.Client(connectionID).SendAsync("receiveKnockoutResult", aliveUsers.Contains(userID));
                }
                else
                {
                    throw new DisconnectException(userID);
                }
            }
        }
    }
}
