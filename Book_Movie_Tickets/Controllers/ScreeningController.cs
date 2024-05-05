
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Book_Movie_Tickets.DTOs;
using Book_Movie_Tickets.Models;


namespace Book_Movie_Tickets.Controllers
{
	public class ScreeningController : Controller

	{

		private readonly DatabaseContext _dbContext;
		public ScreeningController(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}
		// GET: /<controller>/
		public async Task<IActionResult> Index(string? movieName , string searchcustomer)
		{
			var screeningsQuery = _dbContext.Screenings
				.Include(s => s.Theater)
				.Include(s => s.Movie)
				.AsQueryable();
			if (!string.IsNullOrEmpty(movieName))
			{
				screeningsQuery = screeningsQuery.Where(s => s.Movie.title.Contains(movieName));
				ViewBag.MovieName = movieName;
			}
			else
			{
				ViewBag.MovieName = "";
			}


			var list = screeningsQuery.Select(s => new ScreeningDTO
			{
				Id = s.screening_id,
				_title = s.Movie.title,
				_genre = s.Movie.genre,
				_nameTheater = s.Theater.name,
				_start_time = s.start_time,
				_end_time = s.end_time,
				_location = s.Theater.location,
				_image = s.Movie.image
			}).ToList();
			if (list.Count == 0 && !string.IsNullOrEmpty(movieName))
			{
				ViewBag.ErrorMessage = "No screenings found for the provided movie name.";
			}
			else
			{
				ViewBag.ErrorMessage = null;
			}

			return View(list);
		}
		private async Task SelectLists()
		{
			var listMovie = await _dbContext.Movies.ToListAsync();
			var listTheater = await _dbContext.Theaters.ToListAsync();

			ViewBag.listMovieGenre = new SelectList(listMovie, "movies_id", "genre", "movies_id");
			ViewBag.listMovieTitle = new SelectList(listMovie, "movies_id", "title", "movies_id");
			ViewBag.listTheaterName = new SelectList(listTheater, "theater_id", "name", "theater_id");
			ViewBag.listTheaterLocation = new SelectList(listTheater, "theater_id", "location", "theater_id");

			ViewBag.listMovieImage = new SelectList(listTheater, "movies_id", "image", "movies_id");
		}


		public async Task<IActionResult> Create()
		{
			await SelectLists();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(Screenings screening)
		{
			if (ModelState.IsValid)
			{
				await _dbContext.Screenings.AddAsync(screening);
				await _dbContext.SaveChangesAsync();
				TempData["SuccessMessage"] = "Create successful!";

				return RedirectToAction("Index");
			}
			else
			{
				await SelectLists();

				return View();
			}

		}

		public async Task<IActionResult> Delete(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var screening = await _dbContext.Screenings.FindAsync(id);
			if (screening == null)
			{
				return BadRequest("not found");
            }

            _dbContext.Screenings.Remove(screening);
            await _dbContext.SaveChangesAsync();
            TempData["DeleteSuccessMessage"] = "Screening deleted successfully.";
            return RedirectToAction("Index");
        }

		public async Task<IActionResult> Edit(int id)
		{


			var screening = await _dbContext.Screenings.FindAsync(id);

			if (screening == null)
			{
				return NotFound();
			}

			await SelectLists();


			return View(screening);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Screenings screening)
		{
			var listMovie = await _dbContext.Movies.ToListAsync();
			var listTheater = await _dbContext.Theaters.ToListAsync();

			if (ModelState.IsValid)
			{
				_dbContext.Update(screening);
				await _dbContext.SaveChangesAsync();

				TempData["SuccessMessage"] = "Update successful!";
				return RedirectToAction("Index");
			}
			else
			{
				await SelectLists();

				return View(screening);
			}
		}

	}
}
