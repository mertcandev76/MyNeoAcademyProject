using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Areas.Admin.ViewComponents
{
    public class AdminHeaderViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
