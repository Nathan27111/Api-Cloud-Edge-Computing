using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airhockey.Models;

namespace Airhockey.Repositories
{
    public interface ITournamentRepository
    {
        Tournaments GetTournament(string code);

        Tournaments CreateTournament(Tournaments tournament);

        Tournaments UpdateActive(string code);
    }
}
