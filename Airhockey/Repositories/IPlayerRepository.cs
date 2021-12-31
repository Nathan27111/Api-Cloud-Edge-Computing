using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airhockey.Models;

namespace Airhockey.Repositories
{
    public interface IPlayerRepository
    {
        Player CreatePlayer(Player player, string code);
        Player GetPlayer(int id);
        IQueryable<Player> GetPlayersByTournamentId(int tournamentId);
        Player UpdatePlayer(Player player);
    }
}
