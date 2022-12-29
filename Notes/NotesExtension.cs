using CoreXF.Abstractions;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Notes.Data;

using OpenCqs;

using System;

namespace Notes
{
    public class NotesExtension : MvcExtension
    {
        public NotesExtension()
        {
            this.Name = nameof(NotesExtension).Replace("Extension", string.Empty);
            this.Set("Title", this.Name);
            this.Set("Link", "Note/Index");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // db context should be added in host app
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<NotesDbContext>(options => options.UseSqlServer(connectionString));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(NotesExtension).Assembly);
            services.AddHandlers(typeof(NotesExtension).Assembly);
        }
    }
}