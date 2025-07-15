using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Areas.Admin.ViewComponents
{
    public class AdminSidebarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
