using CoreApp.Infrastructure.DBContext;
using CoreApp.Models.CoreModels.SharedEntities;
using CoreApp.SharedAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;


namespace CoreApp.Infrastructure.Entity.Validation
{
    public static class UserValidation
    {
        /// <summary>
        ///     Validates the specified context.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="context">The context.</param>
        /// <param name="isEmailRequired">if set to <c>true</c> [is email required].</param>
        /// <exception cref="System.Exception">This email-ID already registered.</exception>
        public static void Validate(this User user, IDbContext context, bool isEmailRequired = true)
        {
            var exception = new CoreAppException();

            if (!string.IsNullOrEmpty(user.UserName))
                if (context.Table<User>().Any(x => x.UserName == user.UserName && x.UserId != user.UserId))
                    exception.Messages.Add("This username already exist.");

            if (isEmailRequired)
            {
                try
                {
                    new MailAddress(user.Email ?? "");
                }
                catch (FormatException)
                {
                    exception.Messages.Add("Please enter a valid email address");
                }

                if (context.Table<User>().Any(x => x.Email == user.Email && x.UserId != user.UserId))
                    exception.Messages.Add("This email-ID already registered.");
            }

            exception.ThrowIfHasMessages();
        }
    }
}
