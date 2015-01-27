using ThinkEco.Identity.Data.Models;
namespace ThinkEco.Identity.Data.Infrastructure
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    public class AppRoleManager : RoleManager<ApplicationRole>
    {
        public AppRoleManager(RoleStore<ApplicationRole> role) : base(role)
        {

        }

        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            var db = context.Get<ApplicationDbContext>();
            return new AppRoleManager(new RoleStore<ApplicationRole>(db));
        }
    }
}
