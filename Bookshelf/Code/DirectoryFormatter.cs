using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Bookshelf.Code
{
    public class DirectoryFormatter : HtmlDirectoryFormatter
    {
        public DirectoryFormatter() : base(HtmlEncoder.Default)
        {
        }

        public override async Task GenerateContentAsync(HttpContext context, IEnumerable<IFileInfo> contents)
        {
            //return base.GenerateContentAsync(context, contents);
            var razorViewEngine = context.RequestServices.GetRequiredService<IRazorViewEngine>();
            var razorViewEngineOptions = context.RequestServices.GetRequiredService<IOptions<RazorViewEngineOptions>>();
            var tempDataProvider = context.RequestServices.GetRequiredService<ITempDataProvider>();

            var view = new ViewRenderer(razorViewEngine, razorViewEngineOptions, tempDataProvider, context);
            var html = await view.RenderToStringAsync("Directory", contents);

            byte[] bytes = Encoding.UTF8.GetBytes(html);
            context.Response.ContentLength = bytes.Length;
            
            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}