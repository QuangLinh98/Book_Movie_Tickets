using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Movie_Tickets.Models
{
 
    public class Bookings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int booking_id { get; set; }

        public int customer_id { get; set; }

        public int screening_id { get; set; }

        public int number_of_tickets { get; set; }

       
        public  Customers? Customer { get; set; }

      
        public  Screenings? Screening { get; set; }
    }
}
