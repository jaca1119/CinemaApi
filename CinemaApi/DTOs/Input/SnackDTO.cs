using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.DTOs.Input
{
    public enum Size
    {
        Small,
        Medium,
        Large
    }

    public class SnackDTO
    {
        public int Id { get; set; }
        public Size Size { get; set; }
        public int Quantity { get; set; }
    }
}
