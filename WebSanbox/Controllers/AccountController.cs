namespace WebSanbox.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;

    using WebSanbox.Models;

    [Authorize]
    public class AccountController : BaseController
    {
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return this.View("Error", new [] { "Access Denied" });
            }

            if (ModelState.IsValid)
            {
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            if (String.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/admin/index";
            }

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(details.Name, details.Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }
                else
                {
                    var ident =
                        await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);

                    return this.Redirect(returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;
            return this.View(details);
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}