using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Device : BaseDomainModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public int CategoryId { get; set; }

        public required string SerialNumber { get; set; }

        public string? Memo { get; set; }
        public Category Category { get; set; }=null!;

        public List<Property> Properties { get; set; } = [];
    }
}
