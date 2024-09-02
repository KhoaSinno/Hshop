using HSHOP_Ecommerce.Data;
using HSHOP_Ecommerce.Helpers;
using HSHOP_Ecommerce.ViewModals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using HSHOP_Ecommerce.Helpers;

namespace HSHOP_Ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly Hshop2023Context db;


        const string CART_KEY = "CART_HSHOP";
        public List<CartItemVM> CartItems =>
                 HttpContext.Session.Get<List<CartItemVM>>(CART_KEY) ?? new List<CartItemVM>();

        public CartController(Hshop2023Context context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(CartItems);
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var Carts = CartItems;
            var item = Carts.SingleOrDefault(c => c.MaHh == id);
            // Goods not into cart
            if (item == null)
            {
                // IsExist this good ?
                var goods = db.HangHoas.SingleOrDefault(g => g.MaHh == id);
                if (goods == null)
                {
                    TempData["Message"] = $"Không tìm thấy mã hàng hóa có mã {id}";
                    return Redirect("/404");
                }
                // Add item when not into cart
                item = new CartItemVM
                {
                    MaHh = id,
                    TenHh = goods.TenHh,
                    DonGia = goods.DonGia ?? 0,
                    Hinh = goods.Hinh ?? string.Empty,
                    SoLuong = quantity
                };
                Carts.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }
            // Ensure item in cart
            HttpContext.Session.Set<List<CartItemVM>>(CART_KEY, Carts);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveCart(int id)
        {
            var Carts = CartItems;
            var item = Carts.SingleOrDefault(c => c.MaHh == id);
            // Goods not into cart
            if (item != null)
            {
                // IsExist this good ?
                var goods = db.HangHoas.SingleOrDefault(g => g.MaHh == id);
                if (goods == null)
                {
                    TempData["Message"] = $"Không tìm thấy mã hàng hóa có mã {id}";
                    return Redirect("/404");
                }
                // Remove item when not into cart
                Carts.RemoveAll(c => c.MaHh == id);
                // Ensure item in cart
                HttpContext.Session.Set<List<CartItemVM>>(CART_KEY, Carts);
            }
            return RedirectToAction("Index");
        }
    }
}
