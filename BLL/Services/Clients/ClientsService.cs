using AutoMapper;
using BLL.Services.Service;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Clients;
using DAL.Efcore.Repositories.UOW;
using DTO.Models.Clients;

namespace BLL.Services.Clients
{
    public class ClientsService : Service<Client, ClientFullDto, ClientCreateDto, ClientUpdateDto>, IClientsService
    {
        private readonly new IClientsRepository _repository;

        public ClientsService(IUnitOfWork uow, IClientsRepository repository, IMapper mapper) : base(uow, repository, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
