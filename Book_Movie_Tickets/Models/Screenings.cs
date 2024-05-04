using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Book_Movie_Tickets.Models
{
    public class Screenings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int screening_id { get; set; }

        public int movie_id { get; set; }

        public int theater_id { get; set; }

        public DateTime start_time { get; set; }

        public DateTime end_time { get; set; }

        public  Movies? Movie { get; set; }

       
        public  Theaters? Theater { get; set; }

        public ICollection<Bookings>? bookings { get; set; }
    }
}
