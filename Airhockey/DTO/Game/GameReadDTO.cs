using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airhockey.DTO
{
    public class GameReadDTO
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
    }
}
