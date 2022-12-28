using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Notes.Data.Entities;

using System.Security.Claims;

namespace Notes.Data
{
    public class NotesDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public NotesDbContext(DbContextOptions<NotesDbContext> options, IHttpContextAccessor httpContextAccessor = null)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(NotesDbContext).Assembly);
            builder.Entity<Note>().HasQueryFilter(x => x.UserId == this.UserId);
        }

        private string UserId
        {
            get
            {
                var user = this.httpContextAccessor != default
                    ? this.httpContextAccessor.HttpContext?.User
                    : default;

                return user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            }
        }
    }
}