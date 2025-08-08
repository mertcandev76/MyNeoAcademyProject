using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.ViewComponents.Common
{
    public class CommonScriptsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
