using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Airhockey.Models
{
    public partial class Games
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }
        public int? ScorePlayerOne { get; set; }
        public int? ScorePlayerTwo { get; set; }
        public int FaultsPlayerOne { get; set; }
        public int FaultsPlayerTwo { get; set; }
        public int TableId { get; set; }
        public bool IsPlaying { get; set; }
        public int TournamentId { get; set; }

        public virtual AirhockeyTable Table { get; set; }
        public virtual Tournaments Tournament { get; set; }
    }
}
