using AutoMapper;
using BLL.Services.Service;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Producers;
using DAL.Efcore.Repositories.UOW;
using DTO.Models.Producers;

namespace BLL.Services.Producers
{
    public class ProducersService : Service<Producer, ProducerFullDto, ProducerActionDto, ProducerActionDto>, IProducersService
    {
        private readonly new IProducersRepository _repository;

        public ProducersService(IUnitOfWork uow, IProducersRepository repository, IMapper mapper) : base(uow, repository, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
