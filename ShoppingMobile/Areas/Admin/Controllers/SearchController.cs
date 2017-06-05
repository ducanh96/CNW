using ShoppingMobile.Models.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingMobile.Areas.Admin.Controllers
{
    public class SearchController : Controller
    {
        // GET: Admin/Search
        [HttpPost]
        public ActionResult Index(string search)
        {
            using (var db = new DienThoaiDBEntities())
            {
                var TendienThoai = db.DienThoais.Where(x => x.TenDienThoai.Contains(search)).ToList();
                ViewBag.DT = TendienThoai;
                
            }
            return PartialView();
           
        }
    }
}