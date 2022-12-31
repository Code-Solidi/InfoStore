using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

using System;

namespace Bookshelf.ViewComponents
{
    public class BookshelfContents : ViewComponent
    {
        private readonly IRazorViewEngine viewEngine;

        public BookshelfContents(IRazorViewEngine viewEngine)
        {
            this.viewEngine = viewEngine ?? throw new ArgumentNullException(nameof(viewEngine), $"{nameof(viewEngine)} is null.");
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}