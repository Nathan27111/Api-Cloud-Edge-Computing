using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Airhockey.Models;
using Airhockey.DTO.Tournaments;

namespace Airhockey.Mappings
{
    public class TournamentsProfile: Profile
    {
        public TournamentsProfile()
        {
            CreateMap<Tournaments, TournamentReadDTO>();
            CreateMap<TournamentWriteDTO, Tournaments>();
        }
    }
}
