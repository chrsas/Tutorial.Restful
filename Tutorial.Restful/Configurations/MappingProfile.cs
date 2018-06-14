using AutoMapper;
using Tutorial.Restful.Controllers.Dto;
using Tutorial.Restful.Domain.Models;

namespace Tutorial.Restful.Host.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>().ReverseMap();
        }
    }
}