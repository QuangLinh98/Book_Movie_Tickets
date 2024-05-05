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

        public async Task<IActionResult> Index(string? nameSearch)
        {
            var bookingTicket = _databaseContext.bookings
                .Include(b => b.Customer)
                .Include(b => b.Screening)
                .ThenInclude(s => s.Movie)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nameSearch))
            {
                // Thực hiện tìm kiếm theo tên khách hàng
                bookingTicket = bookingTicket.Where(b => b.Customer.name.Contains(nameSearch));
                ViewBag.CustomerName = nameSearch;
            }


            var bookings = await bookingTicket.ToListAsync();

            //Show message on the view 
            var message = TempData["success"] as string;
            ViewBag.Message = message;

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

        private async Task SelecView()
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
        }

        public async Task<IActionResult> Create()
        {

            await SelecView();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Bookings bookings, int numberTicket)
        {
            if (!ModelState.IsValid || bookings.number_of_tickets == null || bookings.number_of_tickets == 0)
            {
                await SelecView();
                ViewBag.MessageError = "Please enter a valid number of tickets (greater than 0).";
                return View();
            }
            else
            {
                await _databaseContext.bookings.AddAsync(bookings);
                await _databaseContext.SaveChangesAsync();
                TempData["success"] = "Booking ticket successfully!";
                return RedirectToAction("Index");
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _databaseContext.bookings.FindAsync(id);
            if (booking == null)
            {
                return BadRequest("Booking null");
            }
            _databaseContext.bookings.Remove(booking);
            await _databaseContext.SaveChangesAsync();
            TempData["success"] = "Delete successfully!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _databaseContext.bookings.FindAsync(id);

            if (booking == null)
            {
                return BadRequest("Booking null");
            }
            else
            {
                await SelecView();
                return View(booking);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Bookings bookings)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                await SelecView();
                _databaseContext.bookings.Update(bookings);
                await _databaseContext.SaveChangesAsync();
                TempData["success"] = "Update successfully!";
                return RedirectToAction("Index");
            }
        }

    }
}
