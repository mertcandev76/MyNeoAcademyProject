using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Areas.Admin.ViewComponents
{
    public class AdminHeadViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
