using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComTam_Store.Models; 

namespace ComTam_Store.Models
{
    //Gio hang
    public class Cart
    {
        public string TenKh { get; set; }
        public string Diachi { get; set; }
        public string SDT { get; set; }
        List<HoaDon> it = new List<HoaDon>();
        List<ChitietHD> items = new List<ChitietHD>();
        public IEnumerable<ChitietHD> Items
        {
            get { return items;  }
        }
        public IEnumerable<HoaDon> Item
        {
            get { return it; }
        }
        public void Add(Product pro, int quantity = 1)
        {
            var item = items.FirstOrDefault(x => x.Product.IdSP == pro.IdSP);
            if (item == null)
            {
                items.Add(new ChitietHD
                {
                    Product = pro,
                    SoLuong = quantity

                });
            }
            else
            {
                item.SoLuong += quantity;
            }
              
        }
        public void Update_Quantity (int id, int quantity)
        {
            var item = items.Find(x => x.Product.IdSP == id);
            if (item != null)
            {
                item.SoLuong = quantity;
            }    

        }
        public double Total_Money()
        {
            var total = items.Sum(x => x.Product.Gia * x.SoLuong);
            return (double)total;
        }public void RemoveCartItem (int id)
        {
            items.RemoveAll(x => x.Product.IdSP == id);

        }
        public int TotalProInCart()
        {
            return (int)items.Sum(x => x.SoLuong);
        }
        public void ClearCart()
        {
            items.Clear();
        }



    }
}