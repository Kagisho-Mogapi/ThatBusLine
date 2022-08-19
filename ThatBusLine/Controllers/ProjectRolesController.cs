using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThatBusLine.Areas.Identity.Data;
using ThatBusLine.Models;

namespace ThatBusLine.Controllers
{
    public class ProjectRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectRolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return roles != null ?
                          View(roles) :
                          Problem("Entity set 'ApplicationDbContext.Role'  is null.");
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRole role)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role.RoleName);
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
            }
            else
            {
                ModelState.AddModelError("", "Role already Exist");
            }
            return ModelState.ErrorCount != 0 ? View() : RedirectToAction("Index");
        }

        public IActionResult SetUserRole()
        {
            List<SelectListItem> rolesList = new();
            int counter = 1;

            foreach (var role in _roleManager.Roles)
            {
                rolesList.Add(new SelectListItem { Value = counter.ToString(), Text = role.Name });
                counter++;
            }


            //assigning SelectListItem to view Bag
            ViewBag.roles = rolesList;

            var users = _userManager.Users;

            return users != null? 
                View(users) :
                Problem("There are no users");
        }
    }
}
