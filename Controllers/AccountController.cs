﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Entities;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager , SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName =  model.UserName   
                };
                var createResult =  await _userManager.CreateAsync(user , model.Password);
                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction("Index" , "Home");
                }
                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        // "" stands for widecard error
                        ModelState.AddModelError("" , error.Description);
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index" , "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               var loginResult =  await _signInManager.PasswordSignInAsync(model.UserName , model.Password , model.RememberMe, false);
               if (loginResult.Succeeded)
               {
                   if (Url.IsLocalUrl(model.ReturnUrl))
                   {
                       return Redirect(model.ReturnUrl);
                   }
                   else
                   {
                       return RedirectToAction("Index" , "Home");
                   }
               }
               ModelState.AddModelError("" , "Fail to login");
            }

            return View(model);

        }
    }
}
