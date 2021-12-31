using Airhockey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airhockey.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly howestairhockeydbContext db;

        public GameRepository(howestairhockeydbContext airhockeyDbContext)
        {
            db = airhockeyDbContext;
        }

        public Games GetGame(int id)
        {
            Games game = db.Games
                            .SingleOrDefault(g => g.Id == id);
            return game;
        }

        public IQueryable<Games> GetGamesByTournamentId(int tournamentId)
        {
            IQueryable<Games> allGamesById = db.Games
                                        .Where(g => g.TournamentId == tournamentId)
                                        .Select(g => g);
            return allGamesById;
        }

        public Games UpdateGame(Games updatedGame)
        {
            Games game = GetGame(updatedGame.Id);
            if (game != null)
            {
                game.ScorePlayerOne = updatedGame.ScorePlayerOne;
                game.ScorePlayerTwo = updatedGame.ScorePlayerTwo;
                game.FaultsPlayerOne = updatedGame.FaultsPlayerOne;
                game.FaultsPlayerTwo = updatedGame.FaultsPlayerTwo;
                game.IsPlaying = updatedGame.IsPlaying;
                Save();
            }
            return game;
        }

        private bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
