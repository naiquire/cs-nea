# CS-NEA
My technical solution for AQA Computer Science NEA 2025. This is a online game where you can improve your writing ability and compete against others
## 🛠️ Game Functions
In general, a player will be given an alphanumeric character and asked to draw it. Their time will be recorded and a server neural network will evaluate the accuracy of their submission.
# Online Multiplayer
- each player has a ranking which is dependent on their time and accuracy (there are many different ranks for different categories)
- 1v1 speed matches (competitive ranked)
- 12 player knockout tournament where last person to submit an answer is knocked out along with all players with an incorrect answer (unranked)
# Offline
- 10, 25, 50 speed trials (speed rank)
- non-timed trials
# Modes
- mirror mode lmaoooo enjoy (mirror rank)
- invisible mode where you cannot see what you are drawing (hidden rank)
## Accounts
Each player has an account, with the details stored in an SQL database. Most things here aren't strictly necessary but if you have time would be nice to have
- i don't really want to add a friends system
- ensure that multitabling is used for grade A
I might add a way to search up accounts and view an about page, along with leaderboards:
- leaderboard for each rank, literally just in numerical order
Account page could also contain graphs detailing rank improvement, which allows for regression to be used

Some other things may be added in the future, but this is more than sufficient for top marks
