using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    public class Weekdays
    {
        public int Id { get; set; }
        public bool Monday { get; set; }
        public bool Sunday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public Doctor Doctor { get; set; }
    }
}
