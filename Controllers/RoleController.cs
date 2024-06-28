using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace movies_ecommerce.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewRole() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRole(RoleViewModel Role)
        {
            if(ModelState.IsValid)
            {
               
                IdentityRole r = new IdentityRole();
                r.Name = Role.RoleName;
                IdentityResult result = await _roleManager.CreateAsync(r);
                if(result.Succeeded)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    foreach(var i in result.Errors)
                    {
                        ModelState.AddModelError("", " Error in something ");
                    }
                }
            }
            return View(Role);
        }
    }
}
