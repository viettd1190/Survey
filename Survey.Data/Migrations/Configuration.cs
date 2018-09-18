using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Survey.Common;
using Survey.Model.Models;

namespace Survey.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SurveyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SurveyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if(!context.Users.Any())
            {
                User user = new User
                            {
                                Username = "admin",
                                Name = "Administrator",
                                Role = UserRole.ADMIN,
                                Status = UserStatus.ACTIVE,
                                FromDate = DateTime.Now
                            };
                string salt = StringHelpers.CreateSalt();
                string password = StringHelpers.CreatePasswordHash("123456",
                                                                   salt);
                user.PasswordSalt = salt;
                user.Password = password;

                context.Users.Add(user);

                context.SaveChanges();
            }
        }
    }
}
