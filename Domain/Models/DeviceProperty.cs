using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DeviceProperty:BaseDomainModel
    {
        public int DeviceId { get; set; }

        public int PropertyId { get; set; }

        public string? Value { get; set; }

        public Device Device { get; set; } = null!;
        public Property Property { get; set; } = null!;
    }
}
