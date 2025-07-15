using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Areas.Admin.ViewComponents
{
    public class AdminRightSidebarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
