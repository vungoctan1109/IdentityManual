using IdentityManual.Data;
using IdentityManual.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentityManual.Controllers
{
    public class UserController : Controller
    {
        private MyIdentityDbContext myIdentityDbContext;
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;

        // GET: User
        public UserController()
        {
            myIdentityDbContext = new MyIdentityDbContext();
            UserStore<User> userStore = new UserStore<User>(myIdentityDbContext);
            userManager = new UserManager<User>(userStore);
            RoleStore<Role> roleStore = new RoleStore<Role>(myIdentityDbContext);
            roleManager = new RoleManager<Role>(roleStore);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(string UserName, string PasswordHash)
        {
            User user = new User()
            {
                UserName = UserName,
            };
            var result = await userManager.CreateAsync(user, PasswordHash);
            if (result.Succeeded)
            {
                return View("ViewSucceeded");
            }
            else
            {
                return View("ViewError");
            }
        }

        public async Task<ActionResult> AddRole(string RoleName)
        {
            Role role = new Role()
            {
                Name = RoleName
            };
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return View("ViewSucceeded");
            }
            else
            {
                return View("ViewError");
            }
        }

        public async Task<ActionResult> AddUserToRole(string UserId, string RoleId)
        {
            var user = myIdentityDbContext.Users.Find(UserId);
            var role = myIdentityDbContext.Roles.Find(RoleId);
            if (role == null || user == null)
            {
                return View("ViewError");
            }
            var result = await userManager.AddToRoleAsync(user.Id, role.Name);
            if (result.Succeeded)
            {
                return View("ViewSucceeded");
            }
            else
            {
                return View("ViewError");
            }
        }

        public async Task<ActionResult> Login(string UserName, string Password)
        {
            var user = await userManager.FindAsync(UserName, Password);
            if (user == null)
            {
                return View("ViewError");
            }
            else
            {
                SignInManager<User, string> signInManager = new SignInManager<User, string>(userManager, Request.GetOwinContext().Authentication);
                await signInManager.SignInAsync(user, false, false);
                return View("ViewSucceeded");
            }
        }

        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/Home");
        }
    }
}