using Bookshelf.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Web;

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
            // https://stackoverflow.com/questions/575440/url-encoding-using-c-sharp
            //var subpath = HttpUtility.UrlEncode(id) ?? string.Empty;
            var subpath = id ?? string.Empty;

            var wwwroot = this.environment.WebRootPath;
            var booksroot = Path.Combine(wwwroot, "books");
            var fileProvider = new PhysicalFileProvider(booksroot);
            var contents = fileProvider.GetDirectoryContents(subpath);

            var map = new Dictionary<string, string>();
            var filemap = Path.Combine(booksroot, subpath, "filemap.json");
            if (System.IO.File.Exists(filemap))
            {
                map = (Dictionary<string, string>)JsonSerializer.Deserialize<IDictionary<string, string>>(System.IO.File.ReadAllText(filemap));
            }
            //else
            //{
            //    map = this.MapFilenamesToUrls(contents, map);
            //}

            map = this.MapFilenamesToUrls(contents, map);
            contents = fileProvider.GetDirectoryContents(subpath);  // get again to fetch renamed

            var json = JsonSerializer.Serialize(map, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(Path.Combine(booksroot, subpath, "filemap.json"), json);

            var model = new BookshelfModel(subpath, contents, map);
            return View(model);
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

                // already encoded? (https://stackoverflow.com/questions/2295223/how-to-find-out-if-string-has-already-been-url-encoded)
                var uriEncoded = filename == HttpUtility.UrlDecode(filename)
                    ? HttpUtility.UrlEncode(filename)   // https://stackoverflow.com/questions/575440/url-encoding-using-c-sharp
                    : filename;

                map[uriEncoded] = filename;
            }

            return map;
        }
    }
}