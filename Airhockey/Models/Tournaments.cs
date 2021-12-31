using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airhockey.Models
{
    public partial class Tournaments
    {
        public Tournaments()
        {
            AirhockeyTable = new HashSet<AirhockeyTable>();
            Games = new HashSet<Games>();
            Player = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int TimePerMatch { get; set; }
        public int PlayerAmount { get; set; }
        public int TableAmount { get; set; }
        public string Code { get; set; }

        public virtual ICollection<AirhockeyTable> AirhockeyTable { get; set; }
        public virtual ICollection<Games> Games { get; set; }
        public virtual ICollection<Player> Player { get; set; }
    }
}
