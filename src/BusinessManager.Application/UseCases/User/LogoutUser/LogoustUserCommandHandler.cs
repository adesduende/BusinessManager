using BusinessManager.Application.Interfaces;
using BusinessManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.UseCases.User.LogoutUser
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public LogoutUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> HandleAsync(LogoutUserCommand request)
        {
            //Remove token from database or cache

            return await Task.FromResult(true);
        }
    }
}
