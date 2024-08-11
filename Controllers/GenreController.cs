using BookNest.Models.Domain;
using BookNest.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService service;
        public GenreController(IGenreService service)
        {
            this.service = service;
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Sucessfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occourd on server side";
            return View(model);
        }


        public IActionResult Update(int id)
        {
            var record = service.FindBy(id);
            return View(record);
        }
        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Sucessfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occourd on server side";
            return View(model);
        }




        public IActionResult Delete(int id)
        {
            var result = service.Delete(id);
            return RedirectToAction("GetAll");
        }
        public IActionResult GetAll()
        {
            var Data = service.GetAll();
            return View(Data);
        }
    }
}
