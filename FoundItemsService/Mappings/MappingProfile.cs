using AutoMapper;
using FoundItemsService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FoundItem, FoundItemDTO>();
            CreateMap<FoundItemDTO, FoundItem>();
        }
    }
}
