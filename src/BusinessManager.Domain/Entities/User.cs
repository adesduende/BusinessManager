using BusinessManager.Domain.Primitives;
using BusinessManager.Domain.ValueObjects;

namespace BusinessManager.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public Nif Nif { get; private set; }
        public Email Email { get; private set; }
        public string Password { get; private set; }

        private readonly List<Role> _roles = new();
        public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
        public User(Guid id, string name, string surname, Nif nif, Email email, string password)
            : base(id)
        {
            Name = name;
            Surname = surname;
            Nif = nif;
            Email = email;
            Password = password;
        }

        public void AddRole(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Role cannot be null.");
            }
            if (_roles.Any(r => r.Id == role.Id))
            {
                throw new InvalidOperationException("Role already exists.");
            }
            _roles.Add(role);
        }
        public void RemoveRole(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Role cannot be null.");
            }
            if (!_roles.Remove(role))
            {
                throw new InvalidOperationException("Role does not exist.");
            }
        }
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            Name = name;
        }
        public void UpdateSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new ArgumentException("Surname cannot be null or empty.", nameof(surname));
            }
            Surname = surname;
        }
        public void UpdatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }
            Password = password;
        }

    }
}
