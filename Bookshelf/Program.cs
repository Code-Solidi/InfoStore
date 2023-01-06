using Bookshelf.Code;

using CoreXF.Abstractions.Base;
using CoreXF.Abstractions.Registry;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;

namespace Bookshelf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddTransient<IExtensionsRegistry, DummyExtensionRegistry>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }

    public class DummyExtensionRegistry : IExtensionsRegistry
    {
        public IEnumerable<IExtension> Extensions { get; }

        public T GetExtension<T>() where T : IExtension
        {
            return default;
        }

        public IExtension GetExtension(string name)
        {
            return default;
        }

        public IExtension GetExtension(Assembly assembly)
        {
            return default;
        }
    }
}