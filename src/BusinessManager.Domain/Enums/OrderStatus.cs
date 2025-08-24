using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.Enums
{
    public enum OrderStatus
    {
        Unknown = 0,
        Pendding = 1,
        Assigned = 2,
        Completed = 3,
        InBudged = 4,
        Finished = 5,                
    }
}
