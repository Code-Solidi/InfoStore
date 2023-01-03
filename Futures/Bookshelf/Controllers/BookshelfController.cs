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

        //public IActionResult Index([FromRoute(Name = "id")] string id = default)
        //[Route(Name =)]
        public IActionResult Index([FromRoute(Name = "id")]string subpath)
        {
            var wwwroot = this.environment.WebRootPath;
            var fileProvider = new PhysicalFileProvider(Path.Combine(wwwroot, "books"));
            var contents = fileProvider.GetDirectoryContents(subpath ?? string.Empty);
            return View(contents);
        }
    }
}