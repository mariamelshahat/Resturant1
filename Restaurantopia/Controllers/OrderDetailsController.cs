using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using Restaurantopia.Entities.Models;
using Restaurantopia.InterFaces;

namespace Restaurantopia.Controllers
{
    public class OrderDetailsController : Controller
    {
        // GET: OrderDetailsController
        private IGenericRepository<OrderDetails> _orderrepository;
        private IGenericRepository<Item> _Rep_Item;


        public OrderDetailsController ( IGenericRepository<OrderDetails> orderrepository, IGenericRepository<Item> rep_Item )
        {
            _orderrepository = orderrepository;
            _Rep_Item = rep_Item;
        }
        /// <summary>
        /// GET: OrderDetailsController/Index
        /// Purpose: Displays a list of all order details.
        /// </summary>
        /// <returns> A view with a list of order details and items.</returns>
        public async Task<ActionResult> Index ()
        {
            var orderDetailsList = await _orderrepository.GetAllAsync ();
            await _Rep_Item.GetAllAsync ();  // Properly await the second async call
            return View ( orderDetailsList );
        }
        /// <summary>
        /// GET: OrderDetailsController/Create
        /// Purpose: Displays the form for creating new order details.
        /// </summary>
        /// <returns>A view for creating new order details.</returns>
        public ActionResult Create ()
        {
            return View ();
        }
        /// <summary>
        /// POST: OrderDetailsController/Create
        /// Purpose: Handles the creation of new order details.
        /// </summary>
        /// <param name="item">The order details to be created.</param>
        /// <returns>Redirects to the Index view on success, or redisplays the form on failure.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create ( OrderDetails item )
        {
            try
            {
                await _orderrepository.AddAsync ( item );
                return RedirectToAction ( nameof ( Index ) );
            }
            catch
            {
                return View ();
            }
        }
        /// <summary>
        /// GET: OrderDetailsController/Edit/{id}
        /// Purpose: Displays the form for editing existing order details based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the order details to edit.</param>
        /// <returns>A view for editing the order details.</returns>
        public async Task<ActionResult> Edit ( int id )
        {
            var orderDetails = await _orderrepository.GetByIdAsync ( id );
            await _Rep_Item.GetAllAsync ();
            if (orderDetails == null)
            {
                return NotFound ();
            }
            return View ( orderDetails );
        }

        /// <summary>
        /// POST: OrderDetailsController/Edit/{id}
        /// Purpose: Handles the update of existing order details.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="updatedOrderDetails">The updated order details.</param>
        /// <returns>Redirects to the Index view on success, or redisplays the form with errors on failure.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit ( int id, OrderDetails updatedOrderDetails )
        {
            try
            {
                // Fetch the existing order details
                var existingOrderDetails = await _orderrepository.GetByIdAsync ( id );
                if (existingOrderDetails == null)
                {
                    return NotFound ();
                }

                // Manual validation for Quantity
                if (updatedOrderDetails.Quantity <= 0)
                {
                    ModelState.AddModelError ( "Quantity", "Quantity must be greater than zero." );
                    return View ( updatedOrderDetails ); // Return view with the error message
                }

                // Update only the quantity
                existingOrderDetails.Quantity = updatedOrderDetails.Quantity;

                // Save the changes
                await _orderrepository.UpdateAsync ( existingOrderDetails );

                return RedirectToAction ( nameof ( Index ) );
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                ModelState.AddModelError ( "", "An error occurred while updating the order details. Please try again." );
                return View ( updatedOrderDetails ); // Return view with the error message
            }
        }
        /// <summary>
        ///  GET: OrderDetailsController/Delete/{id}
        /// Purpose: Displays the confirmation view for deleting specific order details based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>A view to confirm the deletion of the order details.</returns>
        public async Task<ActionResult> Delete ( int id )
        {
            if (id == 0)
            {
                return NotFound ();
            }
            await _Rep_Item.GetAllAsync ();

            OrderDetails D_item = await _orderrepository.GetByIdAsync ( id );
            if (D_item == null)
            {
                return NotFound ();
            }
            return View ( D_item );
        }
        /// <summary>
        /// POST: OrderDetailsController/Delete/{id}
        /// Purpose: Handles the deletion of order details after confirmation.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <param name="order">The order details to confirm deletion.</param>
        /// <returns>Redirects to the Index view on success, or redisplays the view on failure.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete ( int id, OrderDetails order )
        {
            try
            {
                if (id != order.Id)
                {
                    return BadRequest ();
                }

                await _orderrepository.DeleteAsync ( id );
                return RedirectToAction ( nameof ( Index ) );
            }
            catch
            {
                return View ( order );
            }
        }
    }
}