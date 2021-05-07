using CinemaApi.DTOs.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Models.Snacks
{
    public class SnackOrder
    {
        [Key]
        public int Id { get; set; }
        public Snack Snack { get; set; }
        public int Quantity { get; set; }
        public Size Size { get; set; }
    }
}
