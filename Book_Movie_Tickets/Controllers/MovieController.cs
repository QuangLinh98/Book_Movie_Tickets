using Book_Movie_Tickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Movie_Tickets.Controllers
{
    public class MovieController : Controller
    {
        private DatabaseContext _dbContext;
        //IWebHostEnvironment cung cấp thông tin về môi trường hosting của ứng dụng
        private IWebHostEnvironment _webHostEnvironment;
        public MovieController(DatabaseContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _dbContext.Movies.ToListAsync();
            return View(movies);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Movies movie)
        {
            if (!ModelState.IsValid)
            {
                if (movie.ImageFile == null)
                {
                    ModelState.AddModelError("ImageFile", "Image is required");
                }
                return View();
            }
            else
            {
                if (movie.ImageFile != null) // có hình ảnh up lên
                {
                    string imageUniqueName = DateTime.Now.ToString("yyyymmddss")
                                 + "_" + movie.ImageFile.FileName;
                    string uploads = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                    string fullPath = Path.Combine(uploads, imageUniqueName);

                    //FileStream được bao bọc bởi khối using đảm bảo rằng sau khi CopyToAsync hoàn thành thì FileStream sẽ được giải phóng tài nguyên, sẽ đóng file và cập nhật để ghi file đúng cách
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await movie.ImageFile.CopyToAsync(fileStream); // lưu vào folder
                    }
                    movie.image = "image/" + imageUniqueName;
                    await _dbContext.Movies.AddAsync(movie);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Image is required");
                    return View();
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _dbContext.Movies.FindAsync(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (!string.IsNullOrEmpty(product.image))
                {
                    var pathImage = Path.Combine(_webHostEnvironment.WebRootPath, product.image);
                    if (System.IO.File.Exists(pathImage))
                    {
                        System.IO.File.Delete(pathImage);
                    }
                }
                _dbContext.Movies.Remove(product);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            return View(movie);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Movies movie)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (movie.ImageFile != null)
                {
                    string imageUniqueName = DateTime.Now.ToString("yyyymmddss")
                                 + "_" + movie.ImageFile.FileName;
                    string uploads = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                    string fullPath = Path.Combine(uploads, imageUniqueName);
                    //lập trình khối
                    //FileStream đc bao bọc bởi khối using
                    //đảm bảo sau khi CopyToAsync hoàn thành
                    //thì FileStream sẽ được giải phóng tài nguyên để đóng file
                    //và cập nhật để ghi file đúng cách
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await movie.ImageFile.CopyToAsync(fileStream);
                    }

                    // xóa cái cũ rồi thêm cái mới
                    if (!string.IsNullOrEmpty(movie.image))
                    {
                        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, movie.image);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    movie.image = "image/" + imageUniqueName;
                }
                _dbContext.Movies.Update(movie);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }
}
