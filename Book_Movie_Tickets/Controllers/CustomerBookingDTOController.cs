using Book_Movie_Tickets.DTOs;
using Book_Movie_Tickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Book_Movie_Tickets.Controllers
{
    public class CustomerBookingDTOController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public CustomerBookingDTOController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _databaseContext.bookings
                .Include(b => b.Customer)
                .Include(b => b.Screening)
                    .ThenInclude(s => s.Movie)
                   
                .ToListAsync();

         

            var CustomerBookingDTOs = bookings.Select(b => new CustomerBookingDTO
            {
                CustomerBooking_id = b.booking_id,
                _customerName = b.Customer.name,
                _email = b.Customer.email,
                _phone = b.Customer.phone,
                _number_ticket = b.number_of_tickets,
                _Film = b.Screening.Movie.title,
				_start_time = b.Screening.start_time,
                _end_time = b.Screening.end_time,
				_movie_Id = b.Screening.movie_id
            }).ToList();

            return View(CustomerBookingDTOs);
        }

        public async Task<IActionResult> Create()
        {
			
			var listCustomers = await _databaseContext.Customers.ToListAsync();
            ViewBag.Customers_name = new SelectList(listCustomers, "customer_id", "name", "customer_id");
            ViewBag.Customers_email = new SelectList(listCustomers, "customer_id", "email", "customer_id");
            ViewBag.Customers_phone = new SelectList(listCustomers, "customer_id", "phone", "customer_id");


            var listScreening = await _databaseContext.Screenings.ToListAsync();
            ViewBag.Screening_start_time = new SelectList(listScreening, "screening_id", "start_time", "screening_id");
            ViewBag.Screening_end_time = new SelectList(listScreening, "screening_id", "end_time", "screening_id");


            var listMovie = await _databaseContext.Movies.ToListAsync();
 			ViewBag.Movie_name = new SelectList(listMovie, "movies_id", "title", "movies_id");
		

            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(Bookings bookings)
        {
            if (!ModelState.IsValid)
            {
				var listCustomers = await _databaseContext.Customers.ToListAsync();
				var listScreening = await _databaseContext.Screenings.ToListAsync();
				var listMovie = await _databaseContext.Movies.ToListAsync();

				ViewBag.Movie_name = new SelectList(listMovie, "movies_id", "title", "movies_id");
				ViewBag.Customers_name = new SelectList(listCustomers, "customer_id", "name", "customer_id");
				ViewBag.Customers_email = new SelectList(listCustomers, "customer_id", "email", "customer_id");
				ViewBag.Customers_phone = new SelectList(listCustomers, "customer_id", "phone", "customer_id");
				ViewBag.Screening_start_time = new SelectList(listScreening, "screening_id", "start_time", "screening_id");
				ViewBag.Screening_end_time = new SelectList(listScreening, "screening_id", "end_time", "screening_id");

				return View();
            }
            else
            {
                await _databaseContext.bookings.AddAsync(bookings);
                await _databaseContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult>Delete(int id)
        {
            var booking = await _databaseContext.bookings.FindAsync(id);
            if (booking == null)
            {
                return BadRequest("Booking null");
            }
            _databaseContext.bookings.Remove(booking);
            await _databaseContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult>Edit(int id)
        {
            var booking = await _databaseContext.bookings.FindAsync(id);

            if (booking == null)
            {
                return BadRequest("Booking null");
            }
            var listCustomers = await _databaseContext.Customers.ToListAsync();
            var listScreening = await _databaseContext.Screenings.ToListAsync();
            var listMovie = await _databaseContext.Movies.ToListAsync();

            ViewBag.Movie_name = new SelectList(listMovie, "movies_id", "title", "movies_id");
            ViewBag.Customers_name = new SelectList(listCustomers, "customer_id", "name", "customer_id");
            ViewBag.Customers_email = new SelectList(listCustomers, "customer_id", "email", "customer_id");
            ViewBag.Customers_phone = new SelectList(listCustomers, "customer_id", "phone", "customer_id");
            ViewBag.Screening_start_time = new SelectList(listScreening, "screening_id", "start_time", "screening_id");
            ViewBag.Screening_end_time = new SelectList(listScreening, "screening_id", "end_time", "screening_id");

            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult>Edit(Bookings bookings)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var listCustomers = await _databaseContext.Customers.ToListAsync();
                var listScreening = await _databaseContext.Screenings.ToListAsync();
                var listMovie = await _databaseContext.Movies.ToListAsync();

                ViewBag.Movie_name = new SelectList(listMovie, "movies_id", "title", "movies_id");
                ViewBag.Customers_name = new SelectList(listCustomers, "customer_id", "name", "customer_id");
                ViewBag.Customers_email = new SelectList(listCustomers, "customer_id", "email", "customer_id");
                ViewBag.Customers_phone = new SelectList(listCustomers, "customer_id", "phone", "customer_id");
                ViewBag.Screening_start_time = new SelectList(listScreening, "screening_id", "start_time", "screening_id");
                ViewBag.Screening_start_time = new SelectList(listScreening, "screening_id", "end_time", "screening_id");

                _databaseContext.bookings.Update(bookings);
                await _databaseContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

    }
}
