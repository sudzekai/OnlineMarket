using BLL.Services.Service;
using DTO.Models.Producers;

namespace BLL.Services.Producers
{
    public interface IProducersService : IService<ProducerFullDto, ProducerActionDto, ProducerActionDto>
    {
    }
}