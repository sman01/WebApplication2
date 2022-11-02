using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        assignmentEntities1 UserDb = new assignmentEntities1();//Here we are calling Entityframework,in which your selected table place

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(credential objUser)
        {
            if (ModelState.IsValid)
            {
                using (assignmentEntities1 db = new assignmentEntities1())
                {
                    var obj = db.credentials.Where(a => a.uname.Equals(objUser.uname) && a.pass.Equals(objUser.pass)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.id.ToString();
                        Session["UserName"] = obj.uname.ToString();
                        Session["Access"] = obj.access.ToString();
                        return RedirectToAction("UserDetails","User");
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            Session["UserInfo"] = null;
            Session["UserId"] = null;
            Session["access"] = null;
            Session.Abandon();
            return RedirectToAction("Login");
        }

    }
}