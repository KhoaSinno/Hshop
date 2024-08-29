using Microsoft.AspNetCore.Mvc;

namespace HSHOP_Ecommerce.Controllers
{
    public class HangHoaController : Controller
    {
        public IActionResult Index(int? loai)
        {
            return View();
        }
    }
}
