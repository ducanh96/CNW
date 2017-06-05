﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingMobile.Models.ModelDB;

namespace ShoppingMobile.Security
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        public string[] UserAccessRoles { get; set; }
        public AuthorizeRoleAttribute(params string[] Roles)
        {
            UserAccessRoles = Roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            using (var db = new DienThoaiDBEntities())
            {
                foreach (var item in UserAccessRoles)
                {
                    var userRole = (from a in db.UserRoles
                                    join b in db.Table_Role
                                    on a.RoleId equals b.RoleId
                                    join c in db.Table_User
                                    on a.UserId equals c.UserId
                                    where c.UserName == httpContext.User.Identity.Name
                                    where b.RoleName == item
                                    where a.IsActive == true
                                    select a).FirstOrDefault();
                    if (userRole != null)
                    {
                        return true;
                    }


                }
                return false;


            }

        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Admin/User/UnAuthorize");
        }

    } 
}