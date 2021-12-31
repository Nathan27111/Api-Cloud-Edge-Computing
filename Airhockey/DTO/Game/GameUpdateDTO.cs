using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airhockey.DTO
{
    public class GameUpdateDTO
    {
        public int Id { get; set; }
        public int? ScorePlayerOne { get; set; }
        public int? ScorePlayerTwo { get; set; }
        public int FaultsPlayerOne { get; set; }
        public int FaultsPlayerTwo { get; set; }
        public bool IsPlaying { get; set; }
    }
}
