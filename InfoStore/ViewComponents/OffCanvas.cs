using CoreXF.Abstractions.Base;
using CoreXF.Abstractions.Registry;

using InfoStore.Models;

using Microsoft.AspNetCore.Mvc;

namespace InfoStore.ViewComponents
{
    public class OffCanvas : ViewComponent
    {
        private readonly IExtensionsRegistry registry;

        public OffCanvas(IExtensionsRegistry registry)
        {
            this.registry = registry ?? throw new ArgumentNullException(nameof(registry), $"{nameof(registry)} is null.");
        }

        public IViewComponentResult Invoke()
        {
            var model = new List<OffCanvasModel>();
            foreach (var extension in this.registry.Extensions)
            {
                var mvcExtension = extension as AbstractExtension;
                var title = mvcExtension.Get<string>("Title");
                var link = mvcExtension.Get<string>("Link");
                model.Add(new OffCanvasModel(title, link));
            }

            return View(model);
        }
    }
}