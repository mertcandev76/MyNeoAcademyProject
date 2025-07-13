using Microsoft.AspNetCore.Mvc;
namespace MyNeoAcademy.WebUI.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
