using InfoStore.Data.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace InfoStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor = null)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor; // may be null during migrations
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            builder.Entity<Bookmark>().HasQueryFilter(x => x.UserId == this.UserId);
            builder.Entity<Note>().HasQueryFilter(x => x.UserId == this.UserId);
            builder.Entity<ToDo>().HasQueryFilter(x => x.UserId == this.UserId);
        }

        private string UserId => this.httpContextAccessor?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}