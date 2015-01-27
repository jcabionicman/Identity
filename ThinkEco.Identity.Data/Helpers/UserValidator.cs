namespace ThinkEco.Identity.Data.Helpers
{
    using Microsoft.AspNet.Identity;

    using ThinkEco.Identity.Data.Infrastructure;
    using ThinkEco.Identity.Data.Models;

    public static class UserValidator
    {
        public static AppUserManager ValidateUser(this AppUserManager aum, bool allowOnlyAlphaName, bool uniqueEmail)
        {
            aum.UserValidator = new UserValidator<ApplicationUser>(aum)
                                {
                                    AllowOnlyAlphanumericUserNames = allowOnlyAlphaName,
                                    RequireUniqueEmail = uniqueEmail
                                };

            return aum;
        }
    }
}
