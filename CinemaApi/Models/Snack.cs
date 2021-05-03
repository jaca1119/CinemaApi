﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Models
{
    public class Snack
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string imageURL { get; set; }
    }
}
