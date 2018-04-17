using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        AccountModel accountModel = new AccountModel();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account account)
        {
            if (ModelState.IsValid)
            {
                Account temp = accountModel.AuthenticateUser(account);
                if (temp.Role != null)
                {
                    Session["Role"] = temp.Role;
                    return RedirectToAction("Index", "Home");
                }
                else
                    return RedirectToAction("Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(Registration registration)
        {
            if (ModelState.IsValid)
            {
                bool state = accountModel.Registeruser (registration);
                if(state)
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Clear ();
            Session.Abandon ();
            return RedirectToAction ("Login", "Account");
        }
    }
}