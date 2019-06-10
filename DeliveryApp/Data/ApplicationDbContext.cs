using DeliveryApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<DeliveryApp.Models.UserEntity> UserEntity { get; set; }
        public DbSet<DeliveryApp.Models.ActivityEntity> ActivityEntity { get; set; }
        public DbSet<DeliveryApp.Models.RoleEntity> RoleEntity { get; set; }
        public DbSet<DeliveryApp.Models.RoleActivityEntity> RoleActivityEntity { get; set; }
        public DbSet<DeliveryApp.Models.EmployeeEntity> EmployeeEntity { get; set; }
        public DbSet<DeliveryApp.Models.PersonEntity> PersonEntity { get; set; }
        public DbSet<DeliveryApp.Models.PositionEntity> PositionEntity { get; set; }
        public DbSet<DeliveryApp.Models.SynchroEntity> SynchroEntity { get; set; }
        public DbSet<DeliveryApp.Models.DynamicMenuEntity> DynamicMenuEntity { get; set; }
        public DbSet<DeliveryApp.Models.DynamicMenuActivityEntity> DynamicMenuActivityEntity { get; set; }
    }
}
