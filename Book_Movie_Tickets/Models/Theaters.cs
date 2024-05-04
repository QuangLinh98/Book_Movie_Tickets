using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Book_Movie_Tickets.Models
{
    public class Theaters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int theater_id { get; set; }

        public string? name { get; set; }

        public string? location { get; set; }

        public int capacity { get; set; }

        public ICollection<Screenings>? screenings { get; set; }
    }
}
