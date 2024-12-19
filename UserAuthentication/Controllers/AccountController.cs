using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserAuthentication.Models;
using System.Web.Security;

namespace UserAuthentication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using(EMedicineEntities db=new EMedicineEntities())
            {
               var result= db.UserMasters.Where(a => a.UserId == user.Username && a.Password == user.Password);
                string sqlQuery = result.ToString(); // Get the SQL query as a string
               
                if (result.Count()!=0)
                {
                    // Session["Username"] = user.Username;

                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home",user.ToString());
                }
                else
                {
                    TempData["msg"] = "Incorrect UserId/Password";
                }

            }
            return View();
        }


        public ActionResult Logout()
        {
            // Session.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");

        }
    }
}