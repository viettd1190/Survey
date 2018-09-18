using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using Survey.Model.Abstract;
using Survey.Model.Models;

namespace Survey.Data
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext() : base("SurveyConnection")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Faq> Faqs { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<ResultDetail> ResultDetails { get; set; }

        public DbSet<SupportInformation> SupportInformations { get; set; }

        public DbSet<Model.Models.Survey> Surveys { get; set; }

        public override int SaveChanges()
        {
            IEnumerable<DbEntityEntry> modifiedEntries = ChangeTracker.Entries()
                                                                      .Where(x => x.Entity is IAuditable && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (DbEntityEntry entry in modifiedEntries)
            {
                if(entry.Entity is IAuditable entity)
                {
                    string identityName = Thread.CurrentPrincipal.Identity.Name;
                    DateTime now = DateTime.Now;

                    if(entry.State == EntityState.Added)
                    {
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        Entry(entity)
                                .Property(x => x.CreatedDate)
                                .IsModified = false;
                    }

                    entity.UpdatedDate = now;
                }
            }

            return base.SaveChanges();
        }
    }
}
