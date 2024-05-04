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

        public async Task<IActionResult> Index()
        {
            var customer = await _databaseContext.Customers.ToListAsync();
            return View(customer);
        }

        public IActionResult Create()
        {
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
            var customerDLT = await _databaseContext.Customers.FindAsync(id);
            if (!string.IsNullOrEmpty(customerDLT.imagePath))
            {
               var imagePath =Path.Combine(_webHostEnvironment.WebRootPath,"images/", customerDLT.imagePath);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
			_databaseContext.Customers.Remove(customerDLT);
			await _databaseContext.SaveChangesAsync();
			return RedirectToAction("Index");
			
        }

        public async Task<IActionResult> Detail(int id)
        {
            var customer = await _databaseContext.Customers.FirstOrDefaultAsync(c => c.customer_id == id);
            return View(customer);
        }
    }
}
