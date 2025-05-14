using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurantopia.Models;


namespace Restaurantopia.Controllers
{
    [Authorize(Roles = RolesNames.roleAdmin)]
    public class RolesController : Controller
    {

        private readonly UserManager<IdentityUser> user;
        private readonly RoleManager<IdentityRole> roles;

        public RolesController(UserManager<IdentityUser> user, RoleManager<IdentityRole> roles)
        {
            this.user = user;
            this.roles = roles;

        }
        /// <summary>
        /// Purpose: Displays a list of all users in the system.
        /// </summary>
        /// <returns>A view showing a list of users.</returns>
        public async Task<IActionResult> Index()
        {
            var _users = await user.Users.ToListAsync();
            return View(_users);
        }
        /// <summary>
        /// Purpose: Retrieves a specific user by ID and their current roles, and displays a list of all available roles to assign or unassign.
        /// </summary>
        /// <param name="userId"> The ID of the user whose roles are being managed.</param>
        /// <returns>A view showing the user's roles and all available roles for selection.</returns>
        public async Task<IActionResult> addRoles(string userId)
        {
            var _user = await user.FindByIdAsync(userId);
            var userRoles = await user.GetRolesAsync(_user);
            var allRoles = await roles.Roles.ToListAsync();
            if (allRoles != null)
            {
                var roleList = allRoles.Select(r => new roleViewModel()

                {
                    roleId = r.Id,
                    roleName = r.Name,
                    useRole = userRoles.Any(x => x == r.Name)
                }

                );
                ViewBag.userName = _user.UserName;
                ViewBag.userId = userId;
                return View(roleList);
            }
            else
                return NotFound();

        }
        /// <summary>
        /// Purpose: Handles assigning or removing roles for a specific user based on the selected roles from the form.
        /// </summary>
        /// <param name="userId"> The ID of the user being updated.</param>
        /// <param name="jsonRoles"> A JSON string representing the list of roles to assign or unassign.</param>
        /// <returns>Redirects to the Index view after successfully updating the roles, or returns a 404 error if the user is not found.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addRoles(string userId, string jsonRoles)
        {
            var _user = await user.FindByIdAsync(userId);

            List<roleViewModel> myRoles =
                JsonConvert.DeserializeObject<List<roleViewModel>>(jsonRoles);
            if (user != null)
            {
                var userRoles = await user.GetRolesAsync(_user);
                foreach (var role in myRoles)
                {
                    if (userRoles.Any(x => x == role.roleName.Trim()) && !role.useRole)
                    {
                        await user.RemoveFromRoleAsync(_user, role.roleName.Trim());
                    }
                    if (!userRoles.Any(x => x == role.roleName.Trim()) && role.useRole)
                    {
                        await user.AddToRoleAsync(_user, role.roleName.Trim());
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
                return NotFound();
        }
    }
}
