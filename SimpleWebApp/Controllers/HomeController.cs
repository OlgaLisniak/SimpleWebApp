using SimpleWebApp.Models;
using System.Web.Mvc;

namespace SimpleWebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var requestInfo = new RequestInfo
            {
                Type = Request.RequestType,
                IP = Request.UserHostAddress,
                URL = Request.Url.AbsoluteUri
            };

            WriteToFile(requestInfo);

            return View();
        }

        [HttpPost]
        public ActionResult Index(Person person)
        {
            if (ModelState.IsValid)
            {
                var requestInfo = new RequestInfo
                {
                    Type = Request.RequestType,
                    IP = Request.UserHostAddress,
                    URL = Request.Url.AbsoluteUri
                };
                WriteToFile(requestInfo);
            }
            return View();
        }

        [HttpGet]
        public ActionResult FindPerson(int? id)
        {
            var requestInfo = new RequestInfo
            {
                Type = Request.RequestType,
                IP = Request.UserHostAddress,
                URL = Request.Url.AbsoluteUri
            };
            WriteToFile(requestInfo);
            

            return View("Index");
        }

        private void WriteToFile(RequestInfo requestInfo)
        {
            var file = Server.MapPath("~/App_Data/RequestsInfo.txt");
            System.IO.File.AppendAllText(@file, requestInfo.ToString());
        }
    }
}