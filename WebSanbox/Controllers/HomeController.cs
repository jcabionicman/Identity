namespace WebSanbox.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            return View(GetData("Index"));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OtherAction()
        {
            return this.View("Index", GetData("OtherAction"));
        }

        [Authorize]
        public ActionResult UserProps()
        {
            return this.View(CurrentUser);
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<ActionResult> UserProps(ApplicationUser.Cities city)
        //{
        //    var user = CurrentUser;
        //    user.City = city;
        //    user.SetCountryFromCity(city);
        //    await UserManager.UpdateAsync(user);
        //    return this.View(user);
        //}

        private Dictionary<string, object> GetData(string actionName)
        {
            var dict = new Dictionary<string, object>
                       {
                           { "Action", actionName },
                           { "User", this.HttpContext.User.Identity.Name },
                           {
                               "Authenticated",
                               this.HttpContext.User.Identity.IsAuthenticated
                           },
                           {
                               "Auth Type", this.HttpContext.User.Identity.AuthenticationType
                           },
                           { "In Admin Role", this.HttpContext.User.IsInRole("Admin") }
                       };
            return dict;
        } 
    }
}