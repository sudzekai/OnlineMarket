using AutoMapper;
using BLL.Services.Service;
using BLL.Types.Exceptions;
using DAL.Efcore.Models;
using DAL.Efcore.Repositories.Clients;
using DAL.Efcore.Repositories.UOW;
using DTO.Models.Clients;

namespace BLL.Services.Clients
{
    /// <summary>
    /// Сервис для работы с клиентами системы.
    /// </summary>
    /// <remarks>
    /// Расширяет базовый <see cref="Service{TModel, TFullDto, TCreateDto, TUpdateDto}"/>, 
    /// добавляя специфичные методы для аутентификации и поиска пользователей по учетным данным.
    /// </remarks>
    public class ClientsService : Service<Client, ClientFullDto, ClientCreateDto, ClientUpdateDto>, IClientsService
    {
        private readonly new IClientsRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClientsService"/>.
        /// </summary>
        /// <param name="uow">Единица работы для управления транзакциями.</param>
        /// <param name="repository">Репозиторий клиентов.</param>
        /// <param name="mapper">Экземпляр AutoMapper.</param>
        public ClientsService(IUnitOfWork uow, IClientsRepository repository, IMapper mapper) : base(uow, repository, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Получает данные клиента по его логину (email).
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>DTO с данными для аутентификации <see cref="ClientAuthDto"/>.</returns>
        /// <exception cref="RecordNotFoundException">Выбрасывается, если клиент с таким логином не зарегистрирован.</exception>
        public async Task<ClientAuthDto> GetByLoginAsync(string login)
        {
            var client = await _repository.GetByLoginAsync(login)
                ?? throw new RecordNotFoundException($"Клиент с логином {login} не найден");

            return _mapper.Map<ClientAuthDto>(client);
        }

        /// <summary>
        /// Выполняет поиск клиента по логину и паролю для входа в систему.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Полная информация о клиенте в формате <see cref="ClientFullDto"/>.</returns>
        /// <exception cref="RecordNotFoundException">Выбрасывается, если комбинация логина и пароля неверна.</exception>
        public async Task<ClientFullDto> GetByLoginAndPasswordAsync(string login, string password)
        {
            var client = await _repository.GetByLoginAndPasswordAsync(login, password)
                ?? throw new RecordNotFoundException($"Клиент с указанным логином {login} и паролем {password} не найден");

            return _mapper.Map<ClientFullDto>(client);
        }
    }
}