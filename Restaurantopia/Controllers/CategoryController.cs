using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurantopia.InterFaces;
using Restaurantopia.Models;

namespace Restaurantopia.Controllers
{
    public class CategoryController : Controller
    {
        private IGenericRepository<Category> _repository;
        
        public CategoryController(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// GET: List of Categories
        /// Purpose: Fetches and displays all categories available.
        /// </summary>
        /// <returns>A view displaying the list of categories.</returns>
        public async Task<ActionResult> ListOfCategories()
        {
            var Items = await _repository.GetAllAsync();
            return View(Items);
        }
        /// <summary>
        /// GET: Category Details
        /// Purpose: Fetches and displays the details of a specific category based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the category to display.</param>
        /// <returns>A view displaying the details of the specified category.</returns>
        public async Task<ActionResult> Details(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return View(category);
        }
        /// <summary>
        /// GET: Create a new Category
        /// Purpose: Displays the form for creating a new category.
        /// </summary>
        /// <returns>A view to create a new category.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Create a new Category
        /// Purpose: Handles the creation of a new category and saves it to the repository.
        /// </summary>
        /// <param name="item">The category object to be created.</param>
        /// <returns>Redirects to the list of categories upon successful creation, or redisplays the form if creation fails.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category item)
        {
            try
            {
                await _repository.AddAsync(item);
                return RedirectToAction(nameof(ListOfCategories));
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// GET: Edit a Category
        /// Purpose: Displays the form for editing an existing category based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the category to edit.</param>
        /// <returns>A view to edit the specified category.</returns>
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return View(category);
        }

        /// <summary>
        /// POST: Edit a Category
        /// Purpose: Handles the update of an existing category and saves the changes to the repository.
        /// </summary>
        /// <param name="id">The ID of the category to be updated.</param>
        /// <param name="item">The updated category object.</param>
        /// <returns>Redirects to the list of categories upon successful update, or redisplays the form if the update fails.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category item)
        {
            try
            {
                await _repository.UpdateAsync(item);
                return RedirectToAction(nameof(ListOfCategories));
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// GET: Delete a Category
        /// Purpose: Displays a confirmation view to delete a category based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A view to confirm deletion of the specified category.</returns>
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return View(category);
        }

        /// <summary>
        /// POST: Delete a Category
        /// Purpose: Handles the deletion of a category from the repository after confirmation.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <param name="collection">Form collection (usually for additional data, if required).</param>
        /// <returns>Redirects to the list of categories upon successful deletion, or redisplays the form if the deletion fails.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return RedirectToAction(nameof(ListOfCategories));
            }
            catch
            {
                return View();
            }
        }
    }
}
