namespace WebSanbox.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ThinkEco.Identity.Data.Models;

    using WebSanbox.Models;

    [Authorize(Roles = "Administrators")]
    public class RoleAdminController : BaseController
    {
        // GET: RoleAdmin
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Required] string name)
        {
            if (!ModelState.IsValid)
            {
                return this.View(name);
            }

            var result = await RoleManager.CreateAsync(new ApplicationRole(name));
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            this.AddErrorsFromResult(result);
            return this.View(name);
        }

        public async Task<ActionResult> Edit([Required] string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            var memberIds = role.Users.Select(x => x.UserId).ToArray();

            var members = UserManager.Users.Where(x => memberIds.Any(y => y == x.Id));
            var nonmembers = UserManager.Users.Except(members);

            return this.View(new RoleEditModel { Members = members, NonMembers = nonmembers, Role = role });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleModificationModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var role = await RoleManager.FindByNameAsync(model.RoleName);
            if (role == null)
            {
                return this.View("Error", new[] { "Role not found" });
            }

            foreach (var user in model.IdsToAdd ?? new string[]{})
            {
                var result = await UserManager.AddToRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    return this.View(result.Errors);
                }
            }

            foreach (var user in model.IdsToDelete ?? new string[]{})
            {
                var result = await UserManager.RemoveFromRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    return this.View(result.Errors);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete([Required] string id)
        {
            var role = RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return this.View("Error", new[] { "Role name not found" });
            }

            var result = await RoleManager.DeleteAsync(role.Result);
            if (!result.Succeeded)
            {
                this.AddErrorsFromResult(result);
                return this.View("Error", result.Errors);
            }

            return RedirectToAction("Index");
        }
    }
}