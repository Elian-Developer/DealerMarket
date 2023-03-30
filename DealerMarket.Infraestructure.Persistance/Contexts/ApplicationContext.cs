using DealerMarket.Core.Application.Dtos.Account;
using DealerMarket.Core.Application.Helpers;
using DealerMarket.Core.Domain.Common;
using DealerMarket.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Infraestructure.Persistance.Contexts
{
    public class ApplicationContext : DbContext
    {
        //private readonly AuthenticationResponse _userViewModel;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, 
            IHttpContextAccessor httpContextAccessor) : base(options) 
        { 
            _httpContextAccessor = httpContextAccessor;
            //_userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public DbSet<Ads>? Ads { get; set; }
        public DbSet<Category>? Category { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").UserName;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").UserName;
                        break;
                }

            }
                return base.SaveChangesAsync(cancellationToken);
        }

        //Tables Configurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            // Set the table name in database.
            #region Tables  
            modelBuilder.Entity<Ads>().ToTable("Ads");
            modelBuilder.Entity<Category>().ToTable("Category");
            #endregion

            //set the table primary keys.
            #region Primary keys
            modelBuilder.Entity<Ads>().HasKey(ads => ads.Id);
            modelBuilder.Entity<Category>().HasKey(Category => Category.Id);
            #endregion

            //set the table relationship
            #region Relationships
            modelBuilder.Entity<Category>()
                .HasMany<Ads>(category => category.Ads)
                .WithOne(ads => ads.Category)
                .HasForeignKey(ads => ads.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
