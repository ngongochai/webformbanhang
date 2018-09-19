using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace haymuasi.Models
{
    public class Product
    {
        public string categoryid { get; set; }
        public string masp { get; set; }
        public string hinhanh { get; set; }
        public string chitiet { get; set; }
        public long  giasp { get; set; }
        public string price { get; set; }
        public string luotxem { get; set; }
        public string productid { get; set; }
        public string mota { get; set; }
        public int quantity { get; set; }
        public long total { get; set; }
        public string size { get; set; }

        public List<Product> product = new List<Product>();
    }
    public class db
    {
        public string conn = "Data Source=INSPIRON;Initial Catalog=gachongtham;User ID=sa;Password=123";
    }
    public class Order
    {
        public string customerid { get; set; }
        public string namecustomer { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string note { get; set; }
    }
    public class SlideShow
    {
        public string slide1 { get; set; }
        public string slide2 { get; set; }
        public string slide3 { get; set; }
        public string slide4 { get; set; }
        public string slide5 { get; set; }
    }
}