namespace WebSanbox.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using ThinkEco.Identity.Data.Helpers;
    using ThinkEco.Identity.Data.Infrastructure;
    using ThinkEco.Identity.Data.Models;

    public class BaseController : Controller
    {
        protected AppUserManager UserManager
        {
            get
            {
                var um = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                um.CreateValidator(6, false, false, true, true);
                um.ValidateUser(false, true);
                return um;
            }
        }

        protected AppRoleManager RoleManager
        {
            get
            {
                var rm = HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
                return rm;
            }
        }

        protected ApplicationUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }
        protected void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}