using Bookmarks.Data;

using CoreXF.Abstractions;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OpenCqs;

using System;

namespace Bookmarks
{
    public class BookmarksPlugin : MvcExtension
    {
        public BookmarksPlugin()
        {
            this.Name = nameof(BookmarksPlugin).Replace("Plugin", string.Empty);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // db context should be added in host app
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<BookmarksDbContext>(options => options.UseSqlServer(connectionString));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(BookmarksPlugin).Assembly);
            services.AddHandlers(typeof(BookmarksPlugin).Assembly);
        }
    }
}