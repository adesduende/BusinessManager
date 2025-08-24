using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessManager.Domain.ValueObjects
{
    public record Nif
    {
        public string Value { get; init; }

        public Nif(string nif)
        {
            ValidateNif(nif);
            this.Value = nif;
        }

        private void ValidateNif(string nif)
        {
            if(nif == null)
                throw new ArgumentNullException(nameof(nif), "NIF cannot be null.");
            if (!Regex.IsMatch(nif, @"^\d{8}[A-Z]$") || Regex.IsMatch(nif, @"^[A-Z]\d{8}[A-Z]$"))            
                throw new ArgumentException("Invalid NIF format. It must be 8 digits followed by a letter or 9 digits.", nameof(nif));            
        }

        public static implicit operator Nif(string nif) => new Nif(nif);
        public static implicit operator string(Nif nif) => nif.Value;
    }
}
