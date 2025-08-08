using Microsoft.AspNetCore.Mvc;

namespace MyNeoAcademy.WebUI.Areas.Instructor.ViewComponents
{
    public class InstructorSidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
