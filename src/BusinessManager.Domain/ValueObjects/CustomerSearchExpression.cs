using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.ValueObjects
{
    public record CustomerSearchExpression(string? name, string? surname, string? email, string phoneNumber, Address? addres, string? nif);
}
