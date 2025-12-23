using AutoMapper;
using DAL.Efcore.Models;
using DTO.Models.Clients;

namespace BLL.Mappers
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientFullDto>().ReverseMap();
            CreateMap<Client, ClientAuthDto>().ReverseMap();

            CreateMap<Client, ClientCreateDto>().ReverseMap();
            CreateMap<Client, ClientUpdateDto>().ReverseMap();
            CreateMap<Client, ClientUpdateRoleDto>().ReverseMap();
        }
    }
}
