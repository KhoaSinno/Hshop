using HSHOP_Ecommerce.Data;
using HSHOP_Ecommerce.ViewModals;
using Microsoft.AspNetCore.Mvc;

namespace HSHOP_Ecommerce.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly Hshop2023Context db;

        public HangHoaController(Hshop2023Context context)
        {
            db = context;
        }
        public IActionResult Index(int? loai)
        {
            var goods = db.HangHoas.AsQueryable();

            if (loai.HasValue)
            {
                goods = goods.Where(g => g.MaLoai == loai);
            }
            var result = goods.Select(g => new HangHoaVM
            {
                MaHh = g.MaHh,
                TenHh = g.TenHh,
                DonGia = g.DonGia ?? 0,
                Hinh = g.Hinh ?? "",
                MotaNgan = g.MoTaDonVi ?? "",
                TenLoai = g.MaLoaiNavigation.TenLoai

            });
            return View(result);
        }
        public IActionResult Search(string? query)
        {
            var goods = db.HangHoas.AsQueryable();

            if (query != null)
            {
                goods = goods.Where(g => g.TenHh.Contains(query));
            }
            var result = goods.Select(g => new HangHoaVM
            {
                MaHh = g.MaHh,
                TenHh = g.TenHh,
                DonGia = g.DonGia ?? 0,
                Hinh = g.Hinh ?? "",
                MotaNgan = g.MoTaDonVi ?? "",
                TenLoai = g.MaLoaiNavigation.TenLoai

            });
            return View(result);
        }
    }
}
