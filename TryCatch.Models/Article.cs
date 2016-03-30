﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}