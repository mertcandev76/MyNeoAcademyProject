using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Areas.Auth.ViewComponents
{
    public class AuthScriptsViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

}
