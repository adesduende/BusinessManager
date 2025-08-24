using BusinessManager.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.UseCases.Role.AddRole
{
    public record AddRoleCommand (string Name, string Description) : IRequest<Guid?>;
}
