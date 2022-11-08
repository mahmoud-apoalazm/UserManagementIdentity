using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementIdentity.Models;
using UserManagementIdentity.ViewModels;

namespace UserManagementIdentity.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
    
        public async Task<IActionResult> Index()
        {
           
            var users = await _userManager.Users.Select(user =>new UserViewModel
            {
                id=user.Id,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email,
                UserName = user.UserName,
                Roles=_userManager.GetRolesAsync(user).Result

            }).ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> Add()
        {

            var roles = await _roleManager.Roles.Select(r =>new RoleViewModel
            {
                RoleId=r.Id,
                RoleName=r.Name,

            }).ToListAsync();

            var viewmodel = new AddUserViewModel
            {
                Roles = roles

            };
            return View(viewmodel);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            if(!model.Roles.Any( r=> r.IsSelected))
            {
                ModelState.AddModelError("Roles", "Please select at least one role");
                return View(model);
            }

            if(await _userManager.FindByEmailAsync(model.Email) !=null)
            {
                ModelState.AddModelError("Email", "Email is already exists");
                return View(model);
            }
            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "UserName is already exists");
                return View(model);
            }
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                    return View(model);
                }
            }
            await _userManager.AddToRolesAsync(user,model.Roles.Where(r=>r.IsSelected).Select(r=>r.RoleName));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user =await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var viewmodel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    RoleId=role.Id,
                    RoleName=role.Name,
                    IsSelected=_userManager.IsInRoleAsync(user,role.Name).Result
                }).ToList()
            };
        return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            var userroles = await _userManager.GetRolesAsync(user);

            foreach(var role in model.Roles)
            {
                 if(userroles.Any(r =>r==role.RoleName) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                if (!userroles.Any(r => r == role.RoleName) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
            }

  
            return RedirectToAction(nameof(Index)); 
        }

    }
}
