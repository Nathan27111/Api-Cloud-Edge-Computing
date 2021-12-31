using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airhockey.Models;
using Airhockey.DTO;

namespace Airhockey.Mappings
{
    public class GamesProfile : Profile
    {
        public GamesProfile()
        {
            CreateMap<Games, GameReadDTO>();
            CreateMap<GameUpdateDTO, Games>();
        }
    }
}
