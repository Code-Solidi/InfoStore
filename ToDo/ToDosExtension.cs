using CoreXF.Abstractions;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OpenCqs;

using System;

using ToDos.Code;
using ToDos.Data;

namespace ToDos
{
    public class ToDosExtension : MvcExtension
    {
        public ToDosExtension()
        {
            this.Name = nameof(ToDosExtension).Replace("Extension", string.Empty);
            this.Set("Title", this.Name);
            this.Set("Link", "/ToDo/Index");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // db context should be added in host app
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ToDoDbContext>(options => options.UseSqlServer(connectionString));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(ToDosExtension).Assembly);
            services.AddHandlers(typeof(ToDosExtension).Assembly);
            services.AddScoped<ToDoNotifier>();
            services.AddHostedService<TimedBackgroundService>();
        }
    }
}