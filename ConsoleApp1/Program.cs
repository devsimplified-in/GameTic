using System;
using System.Collections.Generic;
using System.Linq;
using tic.data.DBContext;
using tic.data.Model;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            CleanDatabase();
            
            for (int i = 0; i < 10; i++)
            {
                string firstName = Guid.NewGuid().ToString().Substring(0, 8);
                string lastName = Guid.NewGuid().ToString().Substring(0, 8);
                string userID = Guid.NewGuid().ToString().Substring(0, 8);
                string password = Guid.NewGuid().ToString().Substring(0, 8);
                RegisterPlayer(firstName, lastName, userID, password);
            }
            ReadytoStartPlayer();
            StartGame();

            PlayGame();


        }

        private static void CleanDatabase()
        {
            var context = new TictactoegameContext();
            context.Square.RemoveRange(context.Square);
            context.PlayerQueue.RemoveRange(context.PlayerQueue);
            context.PlayerGame.RemoveRange(context.PlayerGame);
            context.Game.RemoveRange(context.Game);
            context.Player.RemoveRange(context.Player);

            context.SaveChanges();
            


        }

        private static void PlayGame()
        {
            var context = new TictactoegameContext();
            var games = context.Game.ToList();

            foreach (var item in games)
            {
                var playerGameidOne = context.PlayerGame.Where(c => c.GameId == item.GameId).First();
                var playerGameidTwo = context.PlayerGame.Max(c => c.PlayerGameId) ;

                for (int i = 1; i <= 9; i++)
                {
                    var Position = context.Square.Where(c => c.PlayerGameId == null).OrderBy(r => Guid.NewGuid()).Take(1).FirstOrDefault();
                    var square = context.Square.Where(t => t.SquareId == Position.SquareId).FirstOrDefault();
                    
                    square.PlayerGameId = i%2 == 0 ?playerGameidOne.PlayerGameId: playerGameidTwo;
                    context.Square.Update(square);
                    context.SaveChanges();
                }
            }

        }

        private static void StartGame()
        {
            var context = new TictactoegameContext();
            while (context.PlayerQueue.Count() >= 2)
            {

                var game = new Game();
                game.Name = Guid.NewGuid().ToString().Substring(0, 8) + DateTime.Now.ToString();
                game.Status = "Active";
                game.CreatedDate = DateTime.Now;
                game.ModifiedDate = DateTime.Now;
                game.CreatedBy = Guid.NewGuid().ToString().Substring(0, 8);
                game.ModifiedBy = Guid.NewGuid().ToString().Substring(0, 8);
                game.RowGuid = Guid.NewGuid();

                context.Game.Add(game);
                context.SaveChanges();

                int gameID = game.GameId;

                var playerQueue = context.PlayerQueue.Take(2).ToList();
                foreach (var item in playerQueue)
                {
                    var playerGame = new PlayerGame();
                    playerGame.GameId = gameID;
                    playerGame.PlayerId = item.PlayerId;
                    playerGame.CreatedDate = DateTime.Now;
                    playerGame.ModifiedDate = DateTime.Now;
                    playerGame.CreatedBy = Guid.NewGuid().ToString().Substring(0, 8);
                    playerGame.ModifiedBy = Guid.NewGuid().ToString().Substring(0, 8);
                    playerGame.RowGuid = Guid.NewGuid();
                    playerGame.Symbol = item.PlayerQueueId % 2 == 0 ? "X" : "O";
                    context.PlayerGame.Add(playerGame);
                    context.PlayerQueue.Remove(item);
                }

                //var square = new Square();
                //square.PositionX = 1;
                //square.PositionY = 1;
                //square.GameId = gameID;
                //square.PlayerGameId = null;
                //square.CreatedDate = DateTime.Now;
                //square.ModifiedDate = DateTime.Now;
                //square.CreatedBy = "Admin";
                //square.ModifiedBy = "Admin";
                //square.RowGuid = Guid.NewGuid();

                var squares = new List<Square>()
                {
                    new Square(){PositionX = 1,PositionY = 1,GameId = gameID,PlayerGameId = null,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = "Admin",ModifiedBy = "Admin",RowGuid = Guid.NewGuid() },
                    new Square(){PositionX = 1,PositionY = 2,GameId = gameID,PlayerGameId = null,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = "Admin",ModifiedBy = "Admin",RowGuid = Guid.NewGuid() },
                    new Square(){PositionX = 1,PositionY = 3,GameId = gameID,PlayerGameId = null,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = "Admin",ModifiedBy = "Admin",RowGuid = Guid.NewGuid() },

                    new Square(){PositionX = 2,PositionY = 1,GameId = gameID,PlayerGameId = null,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = "Admin",ModifiedBy = "Admin",RowGuid = Guid.NewGuid() },
                    new Square(){PositionX = 2,PositionY = 2,GameId = gameID,PlayerGameId = null,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = "Admin",ModifiedBy = "Admin",RowGuid = Guid.NewGuid() },
                    new Square(){PositionX = 2,PositionY = 3,GameId = gameID,PlayerGameId = null,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = "Admin",ModifiedBy = "Admin",RowGuid = Guid.NewGuid() },

                    new Square(){PositionX = 3,PositionY = 1,GameId = gameID,PlayerGameId = null,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = "Admin",ModifiedBy = "Admin",RowGuid = Guid.NewGuid() },
                    new Square(){PositionX = 3,PositionY = 2,GameId = gameID,PlayerGameId = null,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = "Admin",ModifiedBy = "Admin",RowGuid = Guid.NewGuid() },
                    new Square(){PositionX = 3,PositionY = 3,GameId = gameID,PlayerGameId = null,CreatedDate = DateTime.Now,ModifiedDate = DateTime.Now,CreatedBy = "Admin",ModifiedBy = "Admin",RowGuid = Guid.NewGuid() }


                };

                context.Square.AddRange(squares);
                context.SaveChanges();

            }

        }

        private static void ReadytoStartPlayer()
        {
            var context = new TictactoegameContext();

            foreach (var item in context.Player.ToList())
            {
                var playerQueue = new PlayerQueue();
                playerQueue.PlayerId = item.PlayerId;
                playerQueue.CreatedDate = DateTime.Now;
                playerQueue.ModifiedDate = DateTime.Now;
                playerQueue.CreatedBy = item.UserName;
                playerQueue.ModifiedBy = item.UserName;
                playerQueue.RowGuid = Guid.NewGuid();


                context.PlayerQueue.Add(playerQueue);
            }
            context.SaveChanges();
        }

        public static void RegisterPlayer(string firstName, string lastName, string userid, string password)
        {
            var context = new TictactoegameContext();
            var player = new Player();
            player.FirstName = firstName;
            player.LastName = lastName;
            player.UserName = userid;
            player.Password = password;
            player.CreatedDate = DateTime.Now;
            player.ModifiedDate = DateTime.Now;
            player.CreatedBy = userid;
            player.ModifiedBy = userid;
            player.RowGuid = Guid.NewGuid();
            context.Player.Add(player);
            context.SaveChanges();
        }
    }
}
