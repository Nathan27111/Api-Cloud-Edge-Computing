using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airhockey.Models;
using Microsoft.EntityFrameworkCore;

namespace Airhockey.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly howestairhockeydbContext db;

        public PlayerRepository(howestairhockeydbContext airhockeyDbContext)
        {
            db = airhockeyDbContext;
        }
        public Player CreatePlayer(Player newPlayer, string code)
        {
            Tournaments tournament = db.Tournaments.Where(t => t.Code == code)
                                                    .SingleOrDefault();
            if (tournament != null)
            {
                newPlayer.TournamentId = tournament.Id;
                db.Player.Add(newPlayer);
                Save();
            } else
            {
                return null;
            }
            

            return newPlayer;
        }

        public IQueryable<Player> GetPlayersByTournamentId(int tournamentId)
        {
            IQueryable<Player> allPlayersById = db.Player
                                                    .Where(p => p.TournamentId == tournamentId)
                                                    .Select(p => p);
            return allPlayersById;
        }

        public Player GetPlayer(int id)
        {
            Player player = db.Player
                    .SingleOrDefault(p => p.Id == id);
            return player;
        }

        public Player UpdatePlayer(Player updatedPlayer)
        {
            Player player = GetPlayer(updatedPlayer.Id);
            if (player != null)
            {
                player.Ranking = updatedPlayer.Ranking;
                Save();
            }
            return player;
        }

        private bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
