using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Movie_Tickets.Models
{
	public class CustomerBooking
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerBooking_Id { get; set; }
        public int Booking_Id { get; set; }
        public int Screening_id { get; set; }
        public Bookings? Bookings { get; set; }

        public Screenings? Screening { get; set; }
    }
}
