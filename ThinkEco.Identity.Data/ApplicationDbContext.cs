using ThinkEco.Identity.Data.Models;
using ThinkEco.Identity.Data.DatabaseInitializers;

namespace ThinkEco.Identity.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("IdentityDatabase", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            System.Data.Entity.Database.SetInitializer(new IdentityDbInit());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
