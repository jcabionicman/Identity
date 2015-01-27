namespace ThinkEco.Identity.Data.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
        }
    }
}
