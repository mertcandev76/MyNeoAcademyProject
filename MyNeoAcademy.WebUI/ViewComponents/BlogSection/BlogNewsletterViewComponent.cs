using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.ViewComponents.BlogSection
{
    public class BlogNewsletterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
