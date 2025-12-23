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

        public async Task<ClientAuthDto> GetByLoginAsync(string login)
        {
            var client = await _repository.GetByLoginAsync(login) ?? throw new Exception("Клиент с указанным логином не найден");

            return _mapper.Map<ClientAuthDto>(client);
        }

        public async Task<ClientFullDto> GetByLoginAndPasswordAsync(string login, string password)
        {
            var client = await _repository.GetByLoginAndPasswordAsync(login, password) ?? throw new Exception("Клиент с указанным логином и паролем не найден");

            return _mapper.Map<ClientFullDto>(client);
        }
    }
}
