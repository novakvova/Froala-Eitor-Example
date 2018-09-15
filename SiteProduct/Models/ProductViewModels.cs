﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteProduct.Models
{
    public class AddProductViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.10, 100000, ErrorMessage ="Діапазон ціни від 0.10 до 10000.00")]
        public decimal Price { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}