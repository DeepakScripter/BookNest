using BookNest.Models.Domain;
using BookNest.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;


namespace YtBookStore.Controllers
{
    /// <summary>
    /// Controller class for handling author-related actions.
    /// </summary>
    public class AuthorController : Controller
    {
        private readonly IAuthorService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorController"/> class.
        /// </summary>
        /// <param name="service">The author service.</param>
        public AuthorController(IAuthorService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Returns the view for adding a new author.
        /// </summary>
        /// <returns>The add view.</returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Handles the HTTP POST request to add a new author.
        /// </summary>
        /// <param name="model">The author model.</param>
        /// <returns>The add view or redirects to the add action.</returns>
        [HttpPost]
        public IActionResult Add(Author model)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Attempt to add the author
            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occurred on server side";
            return View(model);
        }

        /// <summary>
        /// Returns the view for updating an existing author.
        /// </summary>
        /// <param name="id">The author ID.</param>
        /// <returns>The update view.</returns>
        public IActionResult Update(int id)
        {
            var record = service.FindBy(id);
            return View(record);
        }

        /// <summary>
        /// Handles the HTTP POST request to update an existing author.
        /// </summary>
        /// <param name="model">The updated author model.</param>
        /// <returns>The update view or redirects to the GetAll action.</returns>
        [HttpPost]
        public IActionResult Update(Author model)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Attempt to update the author
            var result = service.Update(model);
            if (result)
            {
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Error has occurred on server side";
            return View(model);
        }

        /// <summary>
        /// Handles the request to delete an author.
        /// </summary>
        /// <param name="id">The author ID.</param>
        /// <returns>Redirects to the GetAll action.</returns>
        public IActionResult Delete(int id)
        {
            var result = service.Delete(id);
            return RedirectToAction("GetAll");
        }

        /// <summary>
        /// Returns the view with all authors.
        /// </summary>
        /// <returns>The view with a list of authors.</returns>
        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return View(data);
        }
    }
}
