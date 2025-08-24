using BusinessManager.Domain.Enums;
using BusinessManager.Domain.Primitives;

namespace BusinessManager.Domain.Entities
{
    public class Order: Entity
    {
        public Guid CustomerId { get; init; }
        public string Description { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime createdAt { get; init; }
        public DateTime updatedAt { get; private set; }

        private readonly List<User> _technicians = new();
        public IReadOnlyCollection<User> Technicians => _technicians.AsReadOnly();


        public Order(
            Guid id,
            Guid customerId,
            string description,
            OrderStatus status = 0)
            : base (id)
        {
            Description = description;
            CustomerId = customerId;
            Status = status;
            createdAt = DateTime.UtcNow;
            updatedAt = DateTime.UtcNow;
        }

        public void AddTechnician(User technician)
        {
            if (technician == null)
            {
                throw new ArgumentNullException(nameof(technician), "Technician cannot be null.");
            }
            if (_technicians.Any(t => t.Id == technician.Id))
            {
                throw new InvalidOperationException("Technician is already assigned to this order.");
            }
            _technicians.Add(technician);
            updatedAt = DateTime.UtcNow;
        }
    }
}
