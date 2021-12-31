using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airhockey.Models;
using Airhockey.DTO;

namespace Airhockey.Mappings
{
    public class TablesProfile : Profile
    {
        public TablesProfile()
        {
            CreateMap<AirhockeyTable, TableReadDTO>();
            CreateMap<TableWriteDTO, AirhockeyTable>();
        }
    }
}
