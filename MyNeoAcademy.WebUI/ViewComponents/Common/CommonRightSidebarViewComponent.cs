using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.ViewComponents.Common
{
    public class CommonRightSidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
