using BusinessManager.Domain.Primitives;
using BusinessManager.Domain.ValueObjects;

namespace BusinessManager.Domain.Entities
{
    public sealed class Customer : Entity
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public Email Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public Nif Nif { get; private set; }
        public Address Address { get; private set; }
        public Customer(Guid id, string name, string surname, Email email, string phoneNumber, Nif nif, Address address)
            : base(id)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            Nif = nif;
            Address = address;
        }
        public Customer(Guid id, string name, string surname, Email email, string phoneNumber, Nif nif)
            : base(id)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            Nif = nif;
            Address = new("","","","","");
        }
    }
}
