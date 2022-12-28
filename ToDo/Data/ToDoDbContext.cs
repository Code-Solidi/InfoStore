using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

using ToDos.Data.Entities;

namespace ToDos.Data
{
    public class ToDoDbContext : DbContext
    {
        //private readonly IHttpContextAccessor httpContextAccessor;

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options/*, IHttpContextAccessor httpContextAccessor = null*/)
            : base(options)
        {
            //this.httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);
            //builder.Entity<ToDo>().HasQueryFilter(x => x.UserId == this.UserId);
        }

        //private string UserId
        //{
        //    get
        //    {
        //        var user = this.httpContextAccessor != default
        //            ? this.httpContextAccessor.HttpContext?.User
        //            : default;

        //        return user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        //    }
        //}
    }
}