using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookNest.Models.Domain;
using BookNest.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.IO;
using System.Linq;

namespace BookNest.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;
        private readonly IPublisherService publisherService;
        public BookController(IBookService bookService, IGenreService genreService, IPublisherService publisherService, IAuthorService authorService)
        {
            this.bookService = bookService;
            this.genreService = genreService;
            this.publisherService = publisherService;
            this.authorService = authorService;
        }
        public IActionResult Add()
        {
            var model = new Book();
            model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString() }).ToList();
            model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString() }).ToList();
            model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Book model)
        {
            model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PubhlisherId }).ToList();
            model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }


        public IActionResult Update(int id)
        {
            var model = bookService.FindById(id);
            model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PubhlisherId }).ToList();
            model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Book model)
        {
            model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PubhlisherId }).ToList();
            model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookService.Update(model);
            if (result)
            {
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }


        public IActionResult Delete(int id)
        {

            var result = bookService.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {

            var data = bookService.GetAll();
            return View(data);
        }


        public IActionResult DownloadExcel()
        {
            // Fetch all books from the service
            var books = bookService.GetAll();

            // Set the license context for EPPlus (NonCommercial or Commercial based on your license)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                // Create a new worksheet named "Books"
                var worksheet = package.Workbook.Worksheets.Add("Books");

                // Add headers to the first row
                worksheet.Cells[1, 1].Value = "Title";
                worksheet.Cells[1, 2].Value = "Genre";
                worksheet.Cells[1, 3].Value = "ISBN";
                worksheet.Cells[1, 4].Value = "Total Pages";
                worksheet.Cells[1, 5].Value = "Author";
                worksheet.Cells[1, 6].Value = "Publisher";

                // Populate the worksheet with book data starting from the second row
                int row = 2;
                foreach (var book in books)
                {
                    worksheet.Cells[row, 1].Value = book.Title;
                    worksheet.Cells[row, 2].Value = book.GenreName;
                    worksheet.Cells[row, 3].Value = book.Isbn;
                    worksheet.Cells[row, 4].Value = book.TotalPages;
                    worksheet.Cells[row, 5].Value = book.AuthorName;
                    worksheet.Cells[row, 6].Value = book.PublisherName;
                    row++;
                }

                // Save the package to a memory stream
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                // Generate a file name using the current date and time
                var fileName = $"Books_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                // Return the file as a download response
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }




    }
}
