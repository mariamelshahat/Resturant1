using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurantopia.InterFaces;
using Restaurantopia.Models;

namespace Restaurantopia.Controllers
{
    public class ReviewController : Controller
    {
        private IGenericRepository<Review> _reviewrepository;

        public ReviewController ( IGenericRepository<Review> reviewrepository )
        {
            _reviewrepository = reviewrepository;
        }
        /// <summary>
        /// Purpose: Displays a form for users to submit a new review.
        /// </summary>
        /// <returns>A view for adding a review.</returns>
        public async Task<IActionResult> Add ()
        {
            return View ();
        }
        /// <summary>
        /// Purpose: Handles the submission of a new review and saves it to the database.
        /// </summary>
        /// <param name="review">The review object containing the user’s feedback.</param>
        /// <returns> Redirects the user back to the Add review page after successfully submitting the review.</returns>
        [HttpPost]
        public async Task<IActionResult> Add ( Review review )
        {
            var i = await _reviewrepository.AddAsync ( review );
			return RedirectToAction ( "Index", "Home" );
		}
    }
}
