using BusinessManager.Domain.Enums;

namespace BusinessManager.Application.Dtos
{
    public class OrderDto
    {
        public string? Id { get; set; }
        public string? Description { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string? CustomerId { get; set; }
        public List<string>? Technicians { get; set; }

    }
}
