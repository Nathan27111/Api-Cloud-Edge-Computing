using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airhockey.Models;

namespace Airhockey.Repositories
{
    public interface ITableRepository
    {
        AirhockeyTable CreateTable(AirhockeyTable table);
        IQueryable<AirhockeyTable> GetTablesByTournamentId(int tournamentId);
    }
}
