namespace WebSanbox.Controllers
{
    using System.Security.Claims;
    using System.Web.Mvc;

    public class ClaimsController : Controller
    {
        [Authorize]
        // GET: Claims
        public ActionResult Index()
        {
            var ident = HttpContext.User.Identity as ClaimsIdentity;
            if (ident == null)
            {
                return View("Error", new string[] { "No claim available" });
            }


            return View(ident.Claims);
        }
    }
}