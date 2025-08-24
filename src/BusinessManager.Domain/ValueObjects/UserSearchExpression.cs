using BusinessManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.ValueObjects
{
    public record UserSearchExpression(string? name, string? surname, string? nif, string? email, Role role);
}
