using Bookshelf.Data;

using CoreXF.Abstractions;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OpenCqs;

using System;

namespace Bookshelf
{
    public class BookshelfExtension : MvcExtension
    {
        public BookshelfExtension()
        {
            this.Name = nameof(BookshelfExtension).Replace("Extension", string.Empty);
            this.Set("Title", this.Name);
            this.Set("Link", "/Bookshelf/Index");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            /*
            // db context should be added in host app
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<BookshelfDbContext>(options => options.UseSqlServer(connectionString));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(BookshelfExtension).Assembly);
            services.AddHandlers(typeof(BookshelfExtension).Assembly);
            */
        }
    }
}