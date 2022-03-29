using CRUDApp.data;
using CRUDApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CRUDApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Index()
        {
            List<MyModel> model = new List<MyModel>();
            amitkumarEntities entities = new amitkumarEntities();
            var res = entities.amit_;
            foreach (var item in res)
            {
                model.Add(new MyModel
                {
                    id = item.id,
                    name = item.name,
                    salary = item.salary,
                    city = item.city,
                    depart = item.depart
                });
            }

            return View(model);
        }
        [HttpGet]
        public ActionResult AddForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddForm(MyModel model)
        {
            amitkumarEntities entities = new amitkumarEntities();
            amit_ table = new amit_();
            table.id = model.id;
            table.name = model.name;
            table.salary = model.salary;
            table.city = model.city;
            table.depart = model.depart;

            if (model.id == 0)
            {
                entities.amit_.Add(table);
                entities.SaveChanges();
            }
            else
            {
                entities.Entry(table).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            MyModel model = new MyModel();
            amitkumarEntities entities = new amitkumarEntities();
            var edit = entities.amit_.Where(m => m.id == id).First();
            model.id = edit.id;
            model.name = edit.name;
            model.salary = edit.salary;
            model.city = edit.city;
            model.depart = edit.depart;

            return View("AddForm", model);
        }
        public ActionResult delete(int id)
        {
            amitkumarEntities entities = new amitkumarEntities();
            var del = entities.amit_.Where(m => m.id == id).First();
            entities.amit_.Remove(del);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Userlog()
        {
            List<UserModel> model = new List<UserModel>();
            amitkumarEntities entities = new amitkumarEntities();
            var res = entities.Table_User;
            foreach (var item in res)
            {
                model.Add(new UserModel
                {
                    id = item.id,
                    name = item.name,
                    email = item.email,
                });
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(UserModel model)
        {
            amitkumarEntities entities = new amitkumarEntities();
            var res = entities.Table_User.Where(m => m.email == model.email).FirstOrDefault();
            if (res.email == null)
            {
                TempData["email"] = "Please enter a valid email";
            }
            else
            {
                if (res.email == model.email && res.password == model.password)
                {
                    FormsAuthentication.SetAuthCookie(model.email, false);
                    Session["username"] = res.name;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    TempData["passwrong"] = "Wrong password";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
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
    }
}