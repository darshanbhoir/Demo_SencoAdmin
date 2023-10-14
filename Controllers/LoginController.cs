using Demo_Senco_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Senco_Admin.Controllers
{
    public class LoginController : Controller
    {
        private SENCO_DB_AdminEntities db = new SENCO_DB_AdminEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult login(string email, string password)
        {
            var query = db.tbl_admin_login.SingleOrDefault(u => u.email == email && u.password == password);
            if(query != null)
            {
                Session["AdminName"] = query.name;
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.Message = "Invalid username or Password";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}