using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Dtos
{
    public sealed class LoginUserDto : UserDto
    {
        public string? Token { get; set; }
    }
}
