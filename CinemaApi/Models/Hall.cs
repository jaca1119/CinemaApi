using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class Hall
    {
        [Key]
        public int Id { get; set; }
        public string HallName { get; set; }
        public List<Row> Rows { get; set; }
    }
}