using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Airhockey.Models;
using Airhockey.DTO;

namespace Airhockey.Mappings
{
    public class PlayersProfile : Profile
    {
        public PlayersProfile()
        {
            CreateMap<Player, PlayerReadDTO>();
            CreateMap<PlayerWriteDTO, Player>();
            CreateMap<PlayerUpdateDTO, Player>();
        }
    }
}
