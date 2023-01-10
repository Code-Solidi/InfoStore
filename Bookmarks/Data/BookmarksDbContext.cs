using Bookmarks.Data.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace Bookmarks.Data
{
    public class BookmarksDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookmarksDbContext(DbContextOptions<BookmarksDbContext> options, IHttpContextAccessor httpContextAccessor = null)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(BookmarksDbContext).Assembly);
            //builder.Entity<Bookmark>().HasQueryFilter(x => x.UserId == this.UserId);
            //builder.Entity<Group>().HasQueryFilter(x => x.UserId == this.UserId);
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