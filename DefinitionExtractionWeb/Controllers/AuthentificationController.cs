using DefinitionExtractionWeb.Models;
using DefinitionExtractionWeb.ViewModels.Authentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DefinitionExtractionWeb.Controllers
{
    public class AuthentificationController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (DEDatabaseEntities db = new DEDatabaseEntities())
                {
                    string passHash = GetHash(model.Password);
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password_hash == passHash);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DEDatabaseEntities db = new DEDatabaseEntities())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (DEDatabaseEntities db = new DEDatabaseEntities())
                    {
                        db.Users.Add(new User { Email = model.Email, First_name = model.FirstName, Last_name=model.LastName, Password_hash = GetHash(model.Password), registration_date = DateTime.Now});
                        db.SaveChanges();

                        string passHash = GetHash(model.Password);
                        user = db.Users.Where(u => u.Email == model.Email && u.Password_hash==passHash).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public static string GetHash(string password) //Получение хэш-значения
        {
            string salt = "$#%$^$#";
            MD5 md5 = new MD5CryptoServiceProvider(); //Экземпляр объекта MD5
            byte[] digest = md5.ComputeHash(Encoding.UTF8.GetBytes(password + salt)); //Вычисление хэш-значения
            string base64digest = Convert.ToBase64String(digest, 0, digest.Length); //Получение строкового значения из массива байт
            return base64digest;
        }

        public bool RightPass(User user, string pass)
        {
            return user.Password_hash == GetHash(pass);
        }
    }
}