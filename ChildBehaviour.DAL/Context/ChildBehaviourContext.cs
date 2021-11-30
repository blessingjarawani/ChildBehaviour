using ChildBehaviour.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChildBehaviour.DAL.Context
{
    public class ChildBehaviourContext : DbContext
    {
        public DbSet<Behaviour> Behaviour { get; set; }
        public DbSet<Symptom> Symptom { get; set; }
        public DbSet<BehaviourRecommendations> BehaviourRecommendations { get; set; }
        public DbSet<BehaviourSymptoms> BehaviourSymptoms { get; set; }
        public DbSet<ChildAssement> ChildAssement { get; set; }
        public DbSet<Pupil> Pupil { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Recommendation> Recommendation { get; set; }
        public ChildBehaviourContext(DbContextOptions<ChildBehaviourContext> options) : base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var added = ChangeTracker.Entries().Where(t => t.State == EntityState.Added)?.ToList();
            added.ForEach(x =>
            {
                x.Property("CreatedDate").CurrentValue = DateTime.Now;
            });

            var updated = ChangeTracker.Entries().Where(t => t.State == EntityState.Modified)?.ToList();
            updated.ForEach(y =>
            {
                y.Property("CreatedDate").IsModified = false;
                y.Property("UpdatedDate").CurrentValue = DateTime.Now;

            });
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
