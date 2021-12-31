using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airhockey.DTO.Tournaments
{
    public class TournamentReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int TimePerMatch { get; set; }
        public int PlayerAmount { get; set; }
        public int TableAmount { get; set; }
        public string Code { get; set; }
    }
}
