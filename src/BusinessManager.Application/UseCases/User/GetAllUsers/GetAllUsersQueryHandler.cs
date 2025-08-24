using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.UseCases.User.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _repository;

        public GetAllUsersQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserDto>> HandleAsync(GetAllUsersQuery request)
        {
            var result = await _repository.GetAllUsersAsync();

            return result.Select(u => UserMapper.ToDto(u));
        }
    }
}
