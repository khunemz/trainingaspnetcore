﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.ViewComponents
{
    public class GreetingViewComponent : ViewComponent
    {
        private IGreeter _greeter;

        public GreetingViewComponent(IGreeter greeter)
        {
            _greeter = greeter;
        }
        public IViewComponentResult Invoke()
        {
            var model = _greeter.GetGreeting();
            return View("Default" , model);
        }
    }
}
