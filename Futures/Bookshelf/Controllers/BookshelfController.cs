using Bookshelf.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

using System;
using System.IO;

namespace Bookshelf.Controllers
{
    public class BookshelfController : Controller
    {
        private readonly IWebHostEnvironment environment;

        public BookshelfController(IWebHostEnvironment environment)
        {
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment), $"{nameof(environment)} is null.");
        }

        public IActionResult Index(string id)
        {
            var subpath = id ?? string.Empty;
            var wwwroot = this.environment.WebRootPath;
            var fileProvider = new PhysicalFileProvider(Path.Combine(wwwroot, "books"));
            var contents = fileProvider.GetDirectoryContents(subpath);
            var model = new BookshelfModel(subpath, contents);
            return View(model);
        }
    }
}