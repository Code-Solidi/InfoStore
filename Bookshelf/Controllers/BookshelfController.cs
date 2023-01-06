using Bookshelf.Models;

using CoreXF.Abstractions.Registry;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

using static System.Net.Mime.MediaTypeNames;

namespace Bookshelf.Controllers
{
    [Authorize]
    public class BookshelfController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly IExtensionsRegistry registry;

        public BookshelfController(IWebHostEnvironment environment, IExtensionsRegistry registry)
        {
            this.registry = registry ?? throw new ArgumentNullException(nameof(registry), $"{nameof(registry)} is null.");
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment), $"{nameof(environment)} is null.");
        }

        public IActionResult Index(string id)
        {
            var userId = this.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var subpath = HttpUtility.UrlDecode((id ?? userId)).Replace('\\', '/');
            var booksRoot = this.GetWebRootPath();
            
            var userBooksRoot = Path.Combine(booksRoot, subpath);
            if (!Directory.Exists(userBooksRoot))
            {
                Directory.CreateDirectory(userBooksRoot);
            }

            var fileProvider = new PhysicalFileProvider(booksRoot);
            var contents = fileProvider.GetDirectoryContents(subpath);

            var map = new Dictionary<string, string>();
            var filemap = Path.Combine(booksRoot, subpath, "filemap.json");
            if (System.IO.File.Exists(filemap))
            {
                map = (Dictionary<string, string>)JsonSerializer.Deserialize<IDictionary<string, string>>(System.IO.File.ReadAllText(filemap));
            }

            map = this.MapFilenamesToUrls(contents, map);
            contents = fileProvider.GetDirectoryContents(subpath);  // get again to fetch renamed

            var json = JsonSerializer.Serialize(map, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(Path.Combine(booksRoot, subpath, "filemap.json"), json);

            var model = new BookshelfModel(subpath, contents, map);
            this.TempData["Path"] = subpath;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(IFormFile file)
        {
            try
            {
                if (await this.UploadFile(file))
                {
                    this.ViewBag.Message = "File Upload Successful";
                }
                else
                {
                    this.ViewBag.Message = "File Upload Failed";
                }
            }
            catch
            {
                // log x
                this.ViewBag.Message = "File Upload Failed";
            }

            var pathRaw = this.TempData["Path"];
            var path = pathRaw is Guid ? ((Guid)pathRaw).ToString("D").ToLowerInvariant() : (string)pathRaw;
            path = !string.IsNullOrEmpty(path) ? $"?id={path}" : string.Empty;
            return this.Redirect($"/Bookshelf/Index/{path}");
        }

        [HttpPost]
        public ActionResult CreateFolder(string directory)
        {
            var booksroot = this.GetWebRootPath();
            //var subpath = (string)this.TempData["Path"];

            var pathRaw = this.TempData["Path"];
            var subpath = pathRaw is Guid ? ((Guid)pathRaw).ToString("D").ToLowerInvariant() : (string)pathRaw;
            try
            {
                directory = Path.Combine(booksroot, subpath, directory);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
            catch
            {
                // log x
                this.ViewBag.Message = "Create Folder Failed";
            }

            var path = !string.IsNullOrEmpty(subpath) ? $"?id={subpath}" : string.Empty;
            return this.Redirect($"/Bookshelf/Index/{path}");
        }

        [HttpPost]
        public ActionResult Back()
        {
            //var path = (string)this.TempData["Path"];

            var pathRaw = this.TempData["Path"];
            var path = pathRaw is Guid ? ((Guid)pathRaw).ToString("D").ToLowerInvariant() : (string)pathRaw;

            var parts = new List<string>(path.Replace('\\', '/').Split('/'));
            var last = parts.Count > 0 ? parts[parts.Count - 1] : default;
            if (last != default)
            {
                parts.Remove(last);
                path = string.Join('/', parts.ToArray());
            }

            path = !string.IsNullOrEmpty(path) ? $"?id={path}" : string.Empty;
            return this.Redirect($"/Bookshelf/Index/{path}");
        }

        private string GetWebRootPath()
        {
            var bookshelfExtension = this.registry.GetExtension<BookshelfExtension>();
            var wwwroot = bookshelfExtension?.Location != null ? Path.Combine(bookshelfExtension?.Location, "wwwroot") : null;
            var path = Path.Combine(wwwroot ?? this.environment.WebRootPath, "books");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        private Dictionary<string, string> MapFilenamesToUrls(IDirectoryContents contents, Dictionary<string, string> map)
        {
            string replaceCSharp(string source)
            {
                var result = source;
                var index = result.IndexOf('#');
                if (index != -1)
                {
                    var sharpSigneIndex = index;
                    while (result[--index] == ' ')
                        ;

                    if (char.ToLowerInvariant(result[index]) == 'c')
                    {
                        result = result.Remove(index, ++sharpSigneIndex - index);
                        result = result.Insert(index, "CSharp");
                    }
                }

                return result;
            }

            foreach (var item in contents.Where(x => x.Name != "filemap.json"))
            {
                // custom rules:
                var filename = replaceCSharp(item.Name);
                // other custom rules here...

                if (filename != item.Name)
                {
                    var source = item.PhysicalPath;
                    var target = source.Replace(item.Name, filename);

                    if (item.IsDirectory)
                    {
                        Directory.Move(source, target);
                    }
                    else
                    {
                        System.IO.File.Move(source, target);
                    }
                }

                var uriEncoded = this.UrlEncode(filename);
                map[uriEncoded] = filename;
            }

            return map;
        }

        private async Task<bool> UploadFile(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || new string[] { ".txt", ".epub", ".pdf" }.All(x => x != ext))
            {
                throw new Exception("File Copy Failed - The extension is invalid");
            }

            var booksroot = this.GetWebRootPath();
            //var subpath = (string)this.TempData["Path"];

            var pathRaw = this.TempData["Path"];
            var subpath = pathRaw is Guid ? ((Guid)pathRaw).ToString("D").ToLowerInvariant() : (string)pathRaw;

            var path = Path.Combine(booksroot, subpath);
            try
            {
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception x)
            {
                throw new Exception("File Copy Failed", x);
            }
        }

        private string UrlEncode(string url)
        {
            // already encoded? (https://stackoverflow.com/questions/2295223/how-to-find-out-if-string-has-already-been-url-encoded)
            return url == HttpUtility.UrlDecode(url)
                ? HttpUtility.UrlEncode(url)   // https://stackoverflow.com/questions/575440/url-encoding-using-c-sharp
                : url;
        }

        private string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }
    }
}