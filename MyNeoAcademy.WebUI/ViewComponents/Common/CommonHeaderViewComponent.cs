using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.ViewComponents.Common
{
    public class CommonHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
