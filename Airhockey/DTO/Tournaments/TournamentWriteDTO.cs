using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airhockey.DTO.Tournaments
{
    public class TournamentWriteDTO
    {
        public string Name { get; set; }
        public int TimePerMatch { get; set; }
        public int PlayerAmount { get; set; }
        public int TableAmount { get; set; }
    }
}
