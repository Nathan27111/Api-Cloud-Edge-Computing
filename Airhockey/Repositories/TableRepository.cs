using Airhockey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airhockey.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly howestairhockeydbContext db;

        public TableRepository(howestairhockeydbContext airhockeyDbContext)
        {
            db = airhockeyDbContext;
        }
        public AirhockeyTable CreateTable(AirhockeyTable newTable)
        {
            db.AirhockeyTable.Add(newTable);
            Save();

            return newTable;
        }

        public IQueryable<AirhockeyTable> GetTablesByTournamentId(int tournamentId)
        {
            IQueryable<AirhockeyTable> allTablesById = db.AirhockeyTable
                                                   .Where(t => t.TournamentId == tournamentId)
                                                   .Select(t => t);
            return allTablesById;
        }

        private bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
