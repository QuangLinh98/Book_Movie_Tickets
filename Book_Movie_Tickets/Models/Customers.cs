using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EDN.Models
{
    public class Customers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int customer_id { get; set; }

        public string? name { get; set; }

        public string? email { get; set; }

        public int phone { get; set; }

        public ICollection<Bookings>? bookings { get; set; }
    }
}
