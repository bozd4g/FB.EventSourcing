using System.Threading.Tasks;
using AutoMapper;
using FB.EventSourcing.Application.Contracts.Users;
using FB.EventSourcing.Application.Contracts.Users.Commands;
using FB.EventSourcing.Application.Contracts.Users.Dtos;
using FB.EventSourcing.Application.Contracts.Users.Queries;
using FB.EventSourcing.Application.Exceptions;
using FB.EventSourcing.Domain.UserAggregate;
using FB.EventSourcing.Persistence;
using Microsoft.Extensions.Logging;

namespace FB.EventSourcing.Application.Users
{
    public class UserAppService : BaseAppService, IUserAppService
    {
        private readonly IRepository<User> _userRepository;

        public UserAppService(IMapper mapper, ILogger<UserAppService> logger,
            IRepository<User> userRepository) : base(mapper, logger)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUser(GetUserByEmailQuery byEmailQuery)
        {
            var user = await _userRepository.FindAsync(e => e.Email == byEmailQuery.Email);
            if (user == null)
                throw new UserFriendlyException("User does not exist!");

            var mappedUser = Mapper.Map<UserDto>(user);

            user.RetrieveDetails();
            mappedUser.Age = user.Detail.Age;
            return mappedUser;
        }

        public async Task<User> CreateUser(CreateUserCommand command)
        {
            var user = new User(command.Name, command.Surname, command.Email, command.Password, command.Birthday);
            var inserted = await _userRepository.AddAsync(user);
            await _userRepository.SaveChanges();

            return inserted;
        }

        public async Task<User> ChangeEmail(ChangeEmailCommand command)
        {
            var user = await _userRepository.GetByUniqueIdAsync(command.UserId);
            if (user == null)
                throw new UserFriendlyException("The user does not found!");

            return user;
        }
    }
}