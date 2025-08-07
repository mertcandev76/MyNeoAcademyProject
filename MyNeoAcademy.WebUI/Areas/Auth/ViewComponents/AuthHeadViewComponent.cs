using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Areas.Auth.ViewComponents
{
    public class AuthHeadViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

  
}
