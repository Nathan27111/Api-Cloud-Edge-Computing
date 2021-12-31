using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airhockey.DTO
{
    public class PlayerReadDTO
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int Ranking { get; set; }
        public int TournamentId { get; set; }
    }
}
