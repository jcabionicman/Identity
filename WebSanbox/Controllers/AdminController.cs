namespace WebSanbox.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ThinkEco.Identity.Data.Models;

    using WebSanbox.Models;

    [Authorize(Roles = "Administrators")]
    public class AdminController : BaseController
    {
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = new ApplicationUser { Email = model.Email, UserName = model.Name };
            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return this.RedirectToAction("Index");
            }

            AddErrorsFromResult(result);
            return this.View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return this.View("Error", new [] { "User not found." });
            }

            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return this.View("Error", result.Errors);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            return this.View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                var validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    this.AddErrorsFromResult(validEmail);
                }


                var validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                if (validPass.Succeeded)
                {
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                }
                else
                {
                    this.AddErrorsFromResult(validPass);
                }

                if ((validEmail.Succeeded && validPass == null)
                    || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    var result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    this.AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found");
            }

            return this.View(user);
        }
    }
}