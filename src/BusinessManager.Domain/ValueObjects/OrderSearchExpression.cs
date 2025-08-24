using BusinessManager.Domain.Entities;
using BusinessManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.ValueObjects;

public record OrderSearchExpression(string? customerId, OrderStatus? status, List<User> technicians);
