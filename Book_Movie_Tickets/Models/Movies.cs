using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Book_Movie_Tickets.Models
{
	public class Movies
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int movies_id { get; set; }

		public string? title { get; set; }

		public string? director { get; set; }

		public DateTime release_date { get; set; }

		public string? genre { get; set; }
		public string? image { get; set; }

		public ICollection<Screenings>? screenings { get; set; }
	}
}