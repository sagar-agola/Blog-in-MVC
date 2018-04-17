using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        DataModel dataModel = new DataModel();
        CommentsModel commentsModel = new CommentsModel();

        public ActionResult Index()
        {
            List<Temp> posts =  dataModel.GetPosts;

            return View(posts);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Post post = dataModel.GetPostDetailByID(id);

            Temp temp = new Temp()
            {
                PostID = post.ID,
                PostName = post.Name,
                PostTime = post.Time,
                PostDetails = post.Detail,
                CommentsList = commentsModel.GetCommentsByID(id)
            };        

            return View(temp);
        }
        [HttpPost]
        public ActionResult Details(Temp temp)
        {
            if(Session["Role"] != null)
            {
                if(Session["Role"].ToString() == "User")
                {
                    Comment comment = new Comment
                    {
                        CommentBody = temp.CommentsList [0].CommentBody,
                        UserName = temp.CommentsList [0].UserName,
                        Time = DateTime.Now,
                        PostId = temp.PostID
                    };
                    commentsModel.AddComment (comment);
                    return RedirectToAction ("Details");
                }
            }
            else
                return RedirectToAction ("Login", "Account");
            return View ();
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session ["Role"] != null)
            {
                if (Session ["Role"].ToString () == "Admin")
                {
                    return View ();
                }
            }
            return RedirectToAction ("Index");
        }
        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                if (Session ["Role"] != null)
                {
                    if (Session ["Role"].ToString () == "Admin")
                    {
                        post.Time = DateTime.Now;
                        dataModel.CreatePost (post);

                        return RedirectToAction ("Index");
                    }
                    else
                        return RedirectToAction ("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session ["Role"] != null)
            {
                if (Session ["Role"].ToString () == "Admin")
                {
                    Post post = dataModel.GetPostDetailByID (id);

                    return View (post);
                }
            }
            return RedirectToAction ("Index");
        }
        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if (Session ["Role"] != null)
            {
                if (Session ["Role"].ToString () == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        dataModel.EditPost (post);

                        return RedirectToAction ("Index");
                    }
                    else
                    {
                        return View ();
                    }
                }
            }
            return RedirectToAction ("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete_Get(int id)
        {
            Post post = dataModel.GetPostDetailByID(id);
            return View(post);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            dataModel.DeletePost(id);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteComment(/*int id*/)
        {
            //commentsModel.DeleteComment (id);
            return RedirectToAction ("Details");
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}