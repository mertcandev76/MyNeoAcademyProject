using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Areas.Admin.ViewComponents
{
    public class AdminFooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
