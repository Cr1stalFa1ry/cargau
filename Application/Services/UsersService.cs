using Core.Enum;
using Core.Interfaces.IRefreshToken;
using Core.Interfaces.Users;
using Core.Models;

namespace Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsersRepository _usersRepository;
        private readonly IRefreshTokenRepository _rtRepository;
        private readonly IRefreshTokenProvider _rtProvider;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserContextService _userContextService;

        // в конструктор надо передавать только интерфейсы
        public UsersService(
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            IRefreshTokenRepository rtRepository,
            IRefreshTokenProvider rtProvider,
            IUserContextService userContextService)
        {
            _passwordHasher = passwordHasher;
            _usersRepository = usersRepository;
            _jwtProvider = jwtProvider;
            _rtProvider = rtProvider;
            _rtRepository = rtRepository;
            _userContextService = userContextService;
        }

        public async Task<User> GetCurrentUser()
        {
            var id = _userContextService.GetCurrentUserId();
            var userId = Guid.Empty;

            if (id.HasValue)
                userId = id.Value;

            return await _usersRepository.GetUser(userId);
        }

        public async Task<(string, string)> Register(string userName, string email, string password, Roles role)
        {
            var hashedPassword = _passwordHasher.Generate(password); // хешируем пароль

            var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email, role); // создаем пользователя

            var token = _jwtProvider.GenerateToken(user); // создаем токен сразу при регистрации
            var refreshToken = _rtProvider.GenerateRefreshToken(user);

            await _usersRepository.Add(user); // сохраняем пользователя и токен обновления в БД
            await _rtRepository.AddToken(refreshToken);

            return (token, refreshToken.Token); // возвращаем токены при регистрации
        }

        public async Task<(string, string)> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (!result)
                throw new Exception("Failed to login");

            var token = _jwtProvider.GenerateToken(user);
            var refreshToken = _rtProvider.GenerateRefreshToken(user);

            await _rtRepository.AddToken(refreshToken);

            return (token, refreshToken.Token);
        }

        public async Task UpdateProfile(Guid id, string newName, string newEmail, Roles role)
        {
            // проверка имени и почты на валидность

            var updateUser = User.Create(id, newName, newEmail, role);

            await _usersRepository.Update(updateUser);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _usersRepository.GetUsers();

            return users;
        }
    }
}
