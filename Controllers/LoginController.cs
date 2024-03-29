﻿using LoginPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LoginPage.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(LoginPage.Models.User userModel)
        {
            using (LoginDataBaseEntities db = new LoginDataBaseEntities())
            {
                var userDetails = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserName; //for saving user login details
                    return RedirectToAction("Index","Home");
                }
            }

            
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"]; //for saving user login details
            Session.Abandon();
            return RedirectToAction("Index", "Login"); 

        }
    }   
}