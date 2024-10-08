﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category:BaseDomainModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List<Property> Properties { get; set; } = [];
    }
}
