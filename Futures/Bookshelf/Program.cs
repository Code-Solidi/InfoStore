using Bookshelf.Code;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

using System.IO;
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
            builder.Services.AddDirectoryBrowser();

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

            // Enable displaying browser links.
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    // todo: get path from settings!
            //    FileProvider = new PhysicalFileProvider("E:\\Books")
            //});

            app.UseRouting();

            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "bookshelf",
            //    pattern: "folder/{id?}", 
            //    defaults: new {controller= "Bookshelf", action = "Index" });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}