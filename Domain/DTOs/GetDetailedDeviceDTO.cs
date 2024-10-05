using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class GetDetailedDeviceDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string SerialNumber { get; set; }
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public string? Memo { get; set; }

        public List<GetDevicePropertiesDTO>? Properties { get; set; } 

    }
}
