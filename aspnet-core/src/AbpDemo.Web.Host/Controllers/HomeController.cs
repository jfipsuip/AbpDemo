using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AbpDemo.Web.Host.Controllers
{
    public class HomeController : AbpController
    {
        public IActionResult Index()
        {
            return Redirect(Request.PathBase.Value + "/swagger");
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public string GetTest()
        {
            return "test api gateway";
        }
    }
}