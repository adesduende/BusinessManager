using BusinessManager.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.UseCases.User.LogoutUser
{
    public record LogoutUserCommand(string token):IRequest<bool>;
}
