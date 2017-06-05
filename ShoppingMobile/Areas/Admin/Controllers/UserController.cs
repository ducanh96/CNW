using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingMobile.Models.ModelDB;
using ShoppingMobile.Models.ViewModel;
using ShoppingMobile.Models.Manager;
using ShoppingMobile.Security;

namespace ShoppingMobile.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeRole(new string[] {"Admin" })]
        public ActionResult CreateUser()
        {
            using (DienThoaiDBEntities db = new DienThoaiDBEntities())
            {
                ViewBag.Role = new SelectList(db.Table_Role.ToList(), "RoleId", "RoleName");
            }
               
            return View();
        }

        public ActionResult UnAuthorize() {
            return View();
        }


        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            ManagerEntities manager = new ManagerEntities();
            using (DienThoaiDBEntities db = new DienThoaiDBEntities())
            {
                if (ModelState.IsValid)
                {
                    if (manager.IsExitstUserName(user.UserName))
                    {
                        ModelState.AddModelError("UserName", "Tên tài khoản đã tồn tại");
                        ViewBag.Role = new SelectList(db.Table_Role.ToList(), "RoleId", "RoleName");
                        return View(user);
                    }
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {


                            var table_user = new Table_User
                            {
                                UserName = user.UserName,
                                FullName = user.FullName,
                                UserPassword = user.UserPassword

                            };
                            db.Table_User.Add(table_user);
                            db.SaveChanges();
                           
                            var userRole = new UserRole
                            {
                                UserId = table_user.UserId,
                                RoleId = user.Role,
                                IsActive = true
                            };
                            db.UserRoles.Add(userRole);
                            db.SaveChanges();
                            trans.Commit();
                            Response.Write("<script>alert('Thêm mới thành công')</script>");
                            return RedirectToAction("Index", "DienThoais");
                        }
                        catch
                        {
                            trans.Rollback();
                        }
                    }
                }
           
      
                ViewBag.Role = new SelectList(db.Table_Role.ToList(), "RoleId", "RoleName");
            }
            return View();
        }
    }
}