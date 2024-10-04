using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Property: BaseDomainModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }



        public List<Category> Categories { get; set; } = [];

        public List<Device> Devices { get; set; } = [];

    }
}
