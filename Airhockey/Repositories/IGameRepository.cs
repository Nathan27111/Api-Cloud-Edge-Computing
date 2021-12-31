using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airhockey.Models;

namespace Airhockey.Repositories
{
    public interface IGameRepository
    {
        Games GetGame(int id);
        IQueryable<Games> GetGamesByTournamentId(int tournamentId);
        Games UpdateGame(Games game);
    }
}
