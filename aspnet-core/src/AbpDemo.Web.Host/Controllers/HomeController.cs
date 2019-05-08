using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpDemo.Web.Host.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect(Request.PathBase.Value + "/swagger");
        }
    }
}
