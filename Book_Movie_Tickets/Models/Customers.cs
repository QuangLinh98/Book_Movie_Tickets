using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Book_Movie_Tickets.Models
{
    public class Customers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int customer_id { get; set; }

        [Required]
        [StringLength(50,MinimumLength =2 ,ErrorMessage = "The name must be between [2-50] characters!")]
        public string? name { get; set; }

        [Required]
        public string? email { get; set; }

        // Số điện thoại
        private string? _phone;

        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Invalid phone number format. Phone number must start with '0' and have 10 digits.")]
        public string? phone
        {
            get { return _phone; }
            set
            {
                // Kiểm tra nếu số điện thoại bắt đầu bằng số 0 và có độ dài là 10
                if (Regex.IsMatch(value, @"^0[0-9]{9}$"))
                {
                    _phone = value;
                }
                else
                {
                    // Nếu không hợp lệ, gán giá trị null
                    _phone = null;
                }
            }
        }

        [NotMapped]
        [Required]
        public IFormFile? imageFile { get; set; }
        public string? imagePath { get; set; }

        public ICollection<Bookings>? bookings { get; set; }
    }
}
