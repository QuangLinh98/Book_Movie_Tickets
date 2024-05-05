using Book_Movie_Tickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Movie_Tickets.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CustomerController(DatabaseContext databaseContext, IWebHostEnvironment webHostEnvironment)
        {
            _databaseContext = databaseContext;
            _webHostEnvironment = webHostEnvironment;
        }

		public async Task<IActionResult> Index(string searchcustomer)
		{
			// Lấy danh sách khách hàng từ cơ sở dữ liệu
			var customers = await _databaseContext.Customers.ToListAsync();

			// Nếu có yêu cầu tìm kiếm, lọc danh sách khách hàng theo từ khóa tìm kiếm
			if (!string.IsNullOrEmpty(searchcustomer))
			{
				customers = customers.Where(c =>
					c.name.IndexOf(searchcustomer, StringComparison.OrdinalIgnoreCase) >= 0 ||
					c.phone.IndexOf(searchcustomer, StringComparison.OrdinalIgnoreCase) >= 0
				).ToList();
			}

			return View(customers);
		}

		public IActionResult Create()
        {
           /* ViewData["Customer"] = new Book_Movie_Tickets.Models.Customers();*/
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customers customers)
        {

            if (!ModelState.IsValid)
            {
                if (customers.imageFile == null)
                {
                    ModelState.AddModelError("imageFile", "Image is required!");
                }
                return View();
            }
            else
            {
                if (customers.imageFile != null)
                {
                    var ImageUniqueName = DateTime.Now.ToString("yyMMddss") + "_" + customers.imageFile.FileName;
                    var upload = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                    string fullpath = Path.Combine(upload, ImageUniqueName);

                    using (var FileStream = new FileStream(fullpath, FileMode.Create))
                    {
                        await customers.imageFile.CopyToAsync(FileStream);
                    }

                    customers.imagePath = "image/" + ImageUniqueName;
                    await _databaseContext.Customers.AddAsync(customers);
                    await _databaseContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }

        }

		public async Task<IActionResult> Delete(int id)
		{
			var customers = await _databaseContext.Customers.FindAsync(id);
			if (customers == null)
			{
				return RedirectToAction("Index");
			}
			else
			{
				if (!string.IsNullOrEmpty(customers.imagePath))
				{
					var pathImage = Path.Combine(_webHostEnvironment.WebRootPath, customers.imagePath);
					if (System.IO.File.Exists(pathImage))
					{
						System.IO.File.Delete(pathImage);
					}
				}
				_databaseContext.Customers.Remove(customers);
				await _databaseContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}

		}

		public async Task<IActionResult> Edit(int id)
		{
			var customer = await _databaseContext.Customers.FindAsync(id);
			if (customer == null)
			{
				return NotFound();
			}
			return View(customer);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Customers customers)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			else
			{
				if (customers.imageFile != null)
				{
					var ImageUniqueName = DateTime.Now.ToString("yyMMddss") + "_" + customers.imageFile.FileName;
					var upload = Path.Combine(_webHostEnvironment.WebRootPath, "image");
					string fullpath = Path.Combine(upload, ImageUniqueName);

					using (var FileStream = new FileStream(fullpath, FileMode.Create))
					{
						await customers.imageFile.CopyToAsync(FileStream);
					}
					if (!string.IsNullOrEmpty(customers.imagePath))
					{
						var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, customers.imagePath);
						if (System.IO.File.Exists(imagePath))
						{
							System.IO.File.Delete(imagePath);
						}
					}

					customers.imagePath = "image/" + ImageUniqueName;

				}
				_databaseContext.Customers.Update(customers);
				await _databaseContext.SaveChangesAsync();
				return RedirectToAction("Index");

			}

		}

		public async Task<IActionResult> Detail(int id)
        {
            var customer = await _databaseContext.Customers.FirstOrDefaultAsync(c => c.customer_id == id);
            return View(customer);
        }

        public IActionResult CreateTicket()
        {
            return RedirectToAction("create", "CustomerBookingDTO");
        }
    }
}
