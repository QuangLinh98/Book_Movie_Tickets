namespace Book_Movie_Tickets.DTOs
{
	public class ScreeningDTO
	{
		public int Id { get; set; }

		public string? _title { get; set; }
		public string? _genre { get; set; }
		public DateTime _start_time { get; set; }
		public DateTime _end_time { get; set; }
		public string? _nameTheater { get; set; }
		public string? _location { get; set; }
		public string? _image { get; set; }
	}
}
