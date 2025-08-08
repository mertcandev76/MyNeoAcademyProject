using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.ViewComponents.Common
{
    public class CommonHeadViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
