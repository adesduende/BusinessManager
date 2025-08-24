using BusinessManager.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Domain.Entities
{
    public sealed class Role : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Role(Guid id, string name, string description)
            : base(id)
        {
            Name = name;
            Description = description;
        }
    }
}
