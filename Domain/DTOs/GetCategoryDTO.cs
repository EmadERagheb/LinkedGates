using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List<GetPropertyDTO>? Properties { get; set; }




    }
}
