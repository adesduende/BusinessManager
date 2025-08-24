using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Domain.Entities;
using BusinessManager.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            var dto = new UserDto
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email.Value,
                Roles = user.Roles.Select(x => x.Name).ToList()
            };
            return dto;
        }
        public static LoginUserDto ToLoginUserDto(User user, string token)
        {
            var dto = new LoginUserDto
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email.Value,
                Roles = user.Roles.Select(x => x.Name).ToList(),
                Token = token
            };
            return dto;
        }
    }
}
