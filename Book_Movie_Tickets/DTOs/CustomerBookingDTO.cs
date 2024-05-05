using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Movie_Tickets.DTOs
{
    public class CustomerBookingDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerBooking_id { get; set; }
        public string? _customerName { get; set; }
        public string? _email { get; set; }
        public string? _phone { get; set; }
        public int _movie_Id { get; set; }
        public int _number_ticket { get; set; }
        public string? _Film { get; set; }
        public DateTime _start_time { get; set; }
        public DateTime _end_time { get; set; }
    }
}
