using UniversityRegistrar.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace UniversityRegistrar.Models
{
    public class UniversityRegistrarContext : IdentityDbContext<User>
    {

        public UniversityRegistrarContext(
            DbContextOptions<UniversityRegistrarContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}