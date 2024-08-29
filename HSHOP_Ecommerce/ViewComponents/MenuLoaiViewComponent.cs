using HSHOP_Ecommerce.Data;
using HSHOP_Ecommerce.ViewModals;
using Microsoft.AspNetCore.Mvc;

namespace HSHOP_Ecommerce.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly Hshop2023Context db;
        public MenuLoaiViewComponent(Hshop2023Context context) { 
        db = context;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(menu => new MenuLoaiVM
            {
               MaLoai = menu.MaLoai, TenLoai = menu.TenLoai, SoLuong = menu.HangHoas.Count
            });
            return View(data); // auto ref Default.cshtml
        }
    }
}
