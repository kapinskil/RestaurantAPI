﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Models
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        public string Street { get; set; }
        [Required]
        [MaxLength(50)]
        public string PostCode { get; set; }
    }
}
