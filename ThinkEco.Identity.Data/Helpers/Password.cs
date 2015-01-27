namespace ThinkEco.Identity.Data.Helpers
{
    using System.Net.Configuration;

    using Microsoft.AspNet.Identity;

    using ThinkEco.Identity.Data.Infrastructure;

    public static class PasswordValidatorHelper
    {
        public static AppUserManager CreateValidator(
            this AppUserManager aum,
            int minLength,
            bool reqSpecialChar,
            bool reqDigit,
            bool reqLowerCase,
            bool reqUpperCase)
        {
            aum.PasswordValidator = new PasswordValidator
                                    {
                                        RequireDigit = reqDigit,
                                        RequireLowercase = reqLowerCase,
                                        RequireNonLetterOrDigit = reqSpecialChar,
                                        RequireUppercase = reqUpperCase,
                                        RequiredLength = minLength
                                    };
            return aum;
        }
    }
}
