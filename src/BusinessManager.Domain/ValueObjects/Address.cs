using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.ValueObjects;

public sealed record Address(string street, string city, string state, string zipCode, string country)
{
    public string Street = street;
    public string City = city;
    public string State = state;
    public string ZipCode = zipCode;
    public string Country = country;
    public override string ToString()
    {
        return $"{Street}, {City}, {State}, {ZipCode}, {Country}";
    }
}

