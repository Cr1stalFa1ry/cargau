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
            // хешируем пароль
            var hashedPassword = _passwordHasher.Generate(password);

            // создаем пользователя
            var user = User.Create(Guid.NewGuid(), role, userName, email, hashedPassword); 

            // создаем токены при регистрации
            var token = _jwtProvider.GenerateToken(user); 
            var refreshToken = _rtProvider.GenerateRefreshToken(user);

            // сохраняем пользователя и токен обновления в БД
            await _usersRepository.Add(user); 
            await _rtRepository.AddToken(refreshToken);

            // возвращаем токены при регистрации
            return (token, refreshToken.Token); 
        }

        public async Task<(string, string)> Login(string email, string password)
        {
            // вытаскиваем пользователя из БД по почте
            var user = await _usersRepository.GetByEmail(email);

            // проверяем пароль на совпадение
            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (!result)
                throw new ArgumentNullException("Failed to login");

            // создаем токены
            var token = _jwtProvider.GenerateToken(user);
            var refreshToken = _rtProvider.GenerateRefreshToken(user);

            await _rtRepository.AddToken(refreshToken);

            return (token, refreshToken.Token);
        }

        public async Task UpdateProfile(Guid id, string newName, string newEmail, Roles role)
        {
            var updateUser = User.Create(id, role, newName, newEmail);
            await _usersRepository.Update(updateUser);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _usersRepository.GetUsers();
            return users;
        }
    }
}
