using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurantopia.InterFaces;
using Restaurantopia.Models;
using Restaurantopia.Repositories;

namespace Restaurantopia.Controllers
{


    public class ItemController : Controller
	{
		private readonly IGenericRepository<Item> _Rep_Item;
		private readonly IGenericRepository<Category> _Rep_Category;
		private readonly IGenericRepository<OrderDetails> _Rep_Order;
		private readonly IWebHostEnvironment _environment;
		private readonly IUploadFile _uploadFile;
		private readonly UserManager<IdentityUser> _userManager;


		public ItemController ( IGenericRepository<Item> repository, IGenericRepository<Category> categoryRep,
			IWebHostEnvironment environment, IUploadFile uploadFile, IGenericRepository<OrderDetails> rep_Order
			, UserManager<IdentityUser> userManager )
		{
			_Rep_Item = repository;
			_Rep_Category = categoryRep;
			_environment = environment;
			_uploadFile = uploadFile;
			_Rep_Order = rep_Order;
			_userManager = userManager;

		}
		/// <summary>
		///  GET: Menu (Displays items with optional category and search filters)
		///   Purpose: Displays a list of items, optionally filtered by category or search query.
		/// </summary>
		/// <param name="categoryId">Filter by category ID.</param>
		/// <param name="searchQuery">Filter by item name or description.</param>
		/// <returns> A view displaying the filtered items and associated categories.</returns>

		public async Task<ActionResult> Menu ( int? categoryId, string searchQuery )
		{
			IEnumerable<Item> Items = await _Rep_Item.GetAllAsync ( includes: new[] { "Category" } );

			if (!string.IsNullOrWhiteSpace ( searchQuery ))
			{
				Items = Items.Where ( item => item.ItemTitle.ToLower ().Contains ( searchQuery.ToLower () ) );
				ViewBag.SearchQuery = searchQuery;
			}
			return View ( Items );
		}

		/// <summary>
		/// GET: Details of an item
		/// Purpose: Displays details of a specific item based on the provided id.
		/// </summary>
		/// <param name="id">The ID of the item to display.</param>
		/// <returns>A view showing the details of the specified item.</returns>
		public async Task<ActionResult> Details ( int id )
		{
			Item item = await _Rep_Item.GetByIdAsync ( id );
			if (item == null)
			{
				return NotFound ();
			}

			ViewBag.C_s = await _Rep_Category.GetAllAsync ();
			return View ( item );
		}

		/// <summary>
		/// GET: Create a new item
		/// Purpose: Displays the form to create a new item.
		/// </summary>
		/// <returns>A view for creating a new item, with a list of available categories.</returns>
		public async Task<ActionResult> Create ()
		{
			var categories = await _Rep_Category.GetAllAsync ();
			var item = new Item () { categoryList = categories.ToList () };
			return View ( item );
		}

		/// <summary>
		/// POST: Create a new item
		/// Purpose: Handles the creation of a new item and saves it to the database.
		/// </summary>
		/// <param name="item">The item object to be created, including optional image upload.</param>
		/// <returns>Redirects to the Menu view on success or redisplays the form with errors on failure.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create ( Item item )
		{
			try
			{
				if (item.ImageFile != null)
				{
					string filePath = await _uploadFile.UploadFileAsync ( "\\Images\\ItemImage\\", item.ImageFile );
					item.ItemImage = filePath;
				}

				await _Rep_Item.AddAsync ( item );
				return RedirectToAction ( nameof ( Menu ) );
			}
			catch (Exception ex)
			{
				ModelState.AddModelError ( string.Empty, ex.Message );
				return View ( item );
			}
		}

		/// <summary>
		/// GET: Edit an item
		/// Purpose: Displays the form to edit an existing item based on the provided id.
		/// </summary>
		/// <param name="id">The ID of the item to be edited.</param>
		/// <returns>A view for editing the item, with the current data pre-filled.</returns>
		public async Task<ActionResult> Edit ( int id )
		{
			ViewBag.C_s = await _Rep_Category.GetAllAsync ();
			if (id == null)
			{
				return NotFound ();
			}
			Item item = await _Rep_Item.GetByIdAsync ( id );
			if (item == null)
			{
				return NotFound ();
			}
			return View ( item );
		}
		/// <summary>
		/// POST: Edit an item
		/// Purpose: Handles the update of an existing item and saves the changes to the database.
		/// </summary>
		/// <param name="item">The updated item object, including optional new image upload.</param>
		/// <returns>Redirects to the Menu view on success or redisplays the form with errors on failure.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit ( Item item )
		{
			try
			{
				if (item.ImageFile != null)
				{
					// If a new image is uploaded, replace the old one
					string filePath = await _uploadFile.UploadFileAsync ( "\\Images\\ItemImage\\", item.ImageFile );
					item.ItemImage = filePath;
				}

				// If no new image is uploaded, the existing image path (item.ItemImage) remains the same

				await _Rep_Item.UpdateAsync ( item );

				return RedirectToAction ( nameof ( Menu ) );
			}
			catch (Exception ex)
			{
				ModelState.AddModelError ( string.Empty, ex.Message );
				ViewBag.C_s = await _Rep_Category.GetAllAsync ();
				return View ( item );
			}
		}


		/// <summary>
		/// GET: Delete confirmation view for an item
		/// Purpose: Displays a confirmation view to delete an item.
		/// </summary>
		/// <param name="id">The ID of the item to be deleted.</param>
		/// <returns>A view showing the item details before confirming the deletion.</returns>
		public async Task<ActionResult> Delete ( int id )
		{
			var item = await _Rep_Item.GetByIdAsync ( id );
			if (item == null)
			{
				return NotFound ();
			}

			ViewBag.C_s = await _Rep_Category.GetAllAsync ();
			return View ( item );
		}

		/// <summary>
		/// POST: Delete an item
		/// Purpose: Handles the deletion of an item from the database after confirmation.
		/// </summary>
		/// <param name="id">The ID of the item to be deleted.</param>
		/// <returns>Redirects to the Menu view on success or redisplays the form with errors on failure.</returns>
		[HttpPost, ActionName ( "Delete" )]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed ( int id )
		{
			try
			{
				var item = await _Rep_Item.GetByIdAsync ( id );
				if (item == null)
				{
					return NotFound ();
				}

				await _Rep_Item.DeleteAsync ( id );
				return RedirectToAction ( nameof ( Menu ) );
			}
			catch (Exception ex)
			{
				ModelState.AddModelError ( string.Empty, ex.Message );
				var item = await _Rep_Item.GetByIdAsync ( id );
				return View ( item );
			}
		}

		/// <summary>
		/// GET: Place an order for an item
		/// Purpose: Displays the form to place an order for a specific item.
		/// </summary>
		/// <param name="id">The ID of the item to be ordered.</param>
		/// <returns>A view showing the order form for the specified item.</returns>
		public async Task<ActionResult> Order ( int id )
		{
			var item = await _Rep_Item.GetByIdAsync ( id );
			if (item == null)
			{
				return NotFound ();
			}

			return View ( item );
		}

		/// <summary>
		/// POST: Place an order
		/// Purpose: Handles the creation of an order for a specific item.
		/// </summary>
		/// <param name="item">The item being ordered, including the quantity.</param>
		/// <returns>Redirects to the OrderDetails view on success or redisplays the form with errors on failure.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<ActionResult> Order ( Item item )
		{
			if (item.Quantity <= 0)
			{
				ModelState.AddModelError ( "Quantity", "Quantity must be greater than zero." );
				return View ( item );
			}

			try
			{
				// Get the currently signed-in user
				var user = await _userManager.GetUserAsync ( User );

				// Retrieve the phone number from the user's profile
				string phoneNumber = await _userManager.GetPhoneNumberAsync ( user );
				if (string.IsNullOrEmpty ( phoneNumber ))
				{
					ModelState.AddModelError ( "", "Phone number is missing in the user profile." );
					return View ( item );
				}

				OrderDetails orderDetails = new OrderDetails
				{
					ItemId = item.Id,
					Quantity = item.Quantity,
					Total = (int)item.ItemPrice * item.Quantity,
					Date = DateTime.Now,
					PhoneNumber = phoneNumber // Set phone number from the signed-in user
				};

				await _Rep_Order.AddAsync ( orderDetails );
				return RedirectToAction ( "Index", "OrderDetails" );
			}
			catch (Exception ex)
			{
				ModelState.AddModelError ( "", ex.Message );
				return View ( item );
			}
		}

	}
}