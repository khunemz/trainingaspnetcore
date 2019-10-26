using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        //[Route(["action"])]
        public string Phone()
        {
            return "13456";
        }

        public string Address()
        {
            return "123 main street";
        }
    }
}
