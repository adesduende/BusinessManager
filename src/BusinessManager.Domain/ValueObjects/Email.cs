using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessManager.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; init; }
        public Email(string email)
        {
            ValidateEmail(email);
            Value = email;
        }
        private void ValidateEmail(string email)
        {
            if (email == null) 
                throw new ArgumentNullException("email"); 
            if (email.Length == 0) 
                throw new ArgumentException("incorrect length email");
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("incorrect format email");
        }

        public static implicit operator Email(string email) => new(email);
        public static implicit operator string(Email email) => email.Value;

    }
}
