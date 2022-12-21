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
    public class ToDosPlugin : MvcExtension
    {
        public ToDosPlugin()
        {
            this.Name = nameof(ToDosPlugin).Replace("Plugin", string.Empty);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // db context should be added in host app
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ToDoDbContext>(options => options.UseSqlServer(connectionString));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            services.AddHandlers(typeof(ToDosPlugin).Assembly);
            services.AddScoped<ToDoNotifier>();
            services.AddHostedService<TimedBackgroundService>();
        }
    }
}