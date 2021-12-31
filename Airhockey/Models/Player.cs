using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airhockey.Models
{
    public partial class Player
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int Ranking { get; set; }
        public int TournamentId { get; set; }

        public virtual Tournaments Tournament { get; set; }
    }
}
