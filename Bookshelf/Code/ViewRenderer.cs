using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshelf.Code
{
    public class ViewRenderer
    {
        private readonly IRazorViewEngine razorViewEngine;
        private readonly ITempDataProvider tempDataProvider;
        private IEnumerable<string> searchedLocations;
        readonly HttpContext httpContext;

        public ViewRenderer(IRazorViewEngine razorViewEngine
            , IOptions<RazorViewEngineOptions> razorViewEngineOptions
            , ITempDataProvider tempDataProvider
            , HttpContext httpContext)
        {
            this.razorViewEngine = razorViewEngine ?? throw new ArgumentNullException(nameof(razorViewEngine), $"{nameof(razorViewEngine)} is null.");
            this.tempDataProvider = tempDataProvider ?? throw new ArgumentNullException(nameof(tempDataProvider), $"{nameof(tempDataProvider)} is null.");
            this.httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext), $"{nameof(httpContext)} is null.");

            _ = razorViewEngineOptions ?? throw new ArgumentNullException(nameof(razorViewEngineOptions), $"{nameof(razorViewEngineOptions)} is null.");
            this.searchedLocations = GetSearchLocations(razorViewEngineOptions);
        }

        public async Task<string> RenderToStringAsync(string viewName, object model)
        {
            var actionContext = new ActionContext(this.httpContext, new RouteData(), new ActionDescriptor());
            var view = this.FindView(viewName);
            using (var output = new StringWriter())
            {
                var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model ?? throw new ArgumentNullException(nameof(model), $"{nameof(model)} is null.") // can be null?
                };

                var tempData = new TempDataDictionary(actionContext.HttpContext, this.tempDataProvider);

                var viewContext = new ViewContext(actionContext, view, viewData, tempData, output, new HtmlHelperOptions());
                await view.RenderAsync(viewContext).ConfigureAwait(false);

                return output.ToString();
            }
        }

        private IView FindView(string viewName)
        {
            var exeFilePath = Environment.ProcessPath;
            (var areaName, var controllerName) = GetAreaAndController(this.httpContext);

            foreach (var format in this.searchedLocations)
            {
                var viewPath = string.Format(format, viewName, controllerName, areaName);
                viewPath = viewPath.Replace(new string('/', 3), "/").Replace(new string('/', 2), "/");
                var result = this.razorViewEngine.GetView(exeFilePath, viewPath, false);
                if (result.Success)
                {
                    return result.View;
                }
            }

            var errorMessage = string.Join(Environment.NewLine
                , new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations));

            throw new InvalidOperationException(errorMessage);
        }

        private static (string, string) GetAreaAndController(HttpContext httpContext)
        {
            var areaName = httpContext.Request.RouteValues.ContainsKey("area")
                ? httpContext.Request.RouteValues["area"].ToString()
                : string.Empty;

            var controllerName = httpContext.Request.RouteValues.ContainsKey("controller")
                ? httpContext.Request.RouteValues["controller"].ToString()
                : string.Empty;

            return (areaName, controllerName);
        }

        private static IEnumerable<string> GetSearchLocations(IOptions<RazorViewEngineOptions> razorViewEngineOptions)
        {
            var formats = new List<string>();
            formats.AddRange(razorViewEngineOptions.Value.ViewLocationFormats);
            formats.AddRange(razorViewEngineOptions.Value.AreaViewLocationFormats);
            formats.AddRange(razorViewEngineOptions.Value.PageViewLocationFormats);
            formats.AddRange(razorViewEngineOptions.Value.AreaPageViewLocationFormats);
            return formats.Distinct();
        }
    }
}