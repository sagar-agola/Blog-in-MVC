using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{   
    public class AdminController : Controller
    {
        DataModel dataModel = new DataModel ();
        AccountModel accountModel = new AccountModel ();
        public ActionResult AdminPanel()
        {
            return View();
        }
        public ActionResult ListAllPosts()
        {
            List<Temp> ListofAllPosts = dataModel.GetPosts;
            return View (ListofAllPosts);
        }
        public ActionResult ListAllUsers()
        {
            List<Account> listOfAllUsers = accountModel.AllUsers;
            return View (listOfAllUsers);
        }
    }
}