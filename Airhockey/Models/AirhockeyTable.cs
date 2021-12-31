using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airhockey.Models
{
    public partial class AirhockeyTable
    {
        public AirhockeyTable()
        {
            Games = new HashSet<Games>();
        }

        public int Id { get; set; }
        public int TournamentId { get; set; }

        public virtual Tournaments Tournament { get; set; }
        public virtual ICollection<Games> Games { get; set; }
    }
}
