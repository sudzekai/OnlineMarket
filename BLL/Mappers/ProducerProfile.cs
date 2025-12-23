using AutoMapper;
using DAL.Efcore.Models;
using DTO.Models.Producers;

namespace BLL.Mappers
{
    public class ProducerProfile : Profile
    {
        public ProducerProfile()
        {
            CreateMap<Producer, ProducerFullDto>().ReverseMap();
            CreateMap<Producer, ProducerActionDto>().ReverseMap();
        }
    }
}
