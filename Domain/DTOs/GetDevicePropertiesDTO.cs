using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class GetDevicePropertiesDTO
    {
        public int PropertyId { get; set; }
        public required string PropertyName { get; set; }
        public  string? Value { get; set; }
    }
}
