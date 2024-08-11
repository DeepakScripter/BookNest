using BookNest.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using BookNest.Models.Domain;
using BookNest.Repositories.Implementation;


namespace BookNest.Controllers
{
    // Controller for managing publishers
    public class PublisherController : Controller
    {
        private readonly IPublisherService service;

        // Constructor injection of IPublisherService
        public PublisherController(IPublisherService service)
        {
            this.service = service;
        }

        // GET: Publisher/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Publisher/Add
        [HttpPost]
        public IActionResult Add(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occurred on server side";
            return View(model);
        }

        // GET: Publisher/Update/{id}
        public IActionResult Update(int id)
        {
            var record = service.FindById(id);
            return View(record);
        }

        // POST: Publisher/Update
        [HttpPost]
        public IActionResult Update(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
            if (result)
            {
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Error has occurred on server side";
            return View(model);
        }

        // GET: Publisher/Delete/{id}
        public IActionResult Delete(int id)
        {
            var result = service.Delete(id);
            return RedirectToAction("GetAll");
        }

        // GET: Publisher/GetAll
        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return View(data);
        }
    }
}
