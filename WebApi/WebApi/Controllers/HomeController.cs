using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly string urlApi = "http://localhost:62217/api/product";

        public ActionResult Index()
        {
            var requestInfo = new RequestInfo
            {
                Type = Request.RequestType,
                IP = Request.UserHostAddress,
                URL = Request.Url.AbsoluteUri
            };
            WriteToFile(requestInfo);

            IEnumerable<Product> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                //HTTP GET
                var responseTask = client.GetAsync("product");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Product>>();
                    readTask.Wait();

                    products = readTask.Result;
                }
                else
                {
                    products = Enumerable.Empty<Product>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(products);
        }

        public ActionResult Create()
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
        public ActionResult Create(Product product)
        {
            var requestInfo = new RequestInfo
            {
                Type = Request.RequestType,
                IP = Request.UserHostAddress,
                URL = Request.Url.AbsoluteUri
            };
            WriteToFile(requestInfo);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Product>("product", product);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var requestInfo = new RequestInfo
            {
                Type = Request.RequestType,
                IP = Request.UserHostAddress,
                URL = Request.Url.AbsoluteUri
            };
            WriteToFile(requestInfo);

            Product product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                //HTTP GET
                var responseTask = client.GetAsync("product?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            var requestInfo = new RequestInfo
            {
                Type = Request.RequestType + "+Put",
                IP = Request.UserHostAddress,
                URL = Request.Url.AbsoluteUri
            };
            WriteToFile(requestInfo);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Product>("product", product);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var requestInfo = new RequestInfo
            {
                Type = Request.RequestType + "+Delete",
                IP = Request.UserHostAddress,
                URL = Request.Url.AbsoluteUri
            };
            WriteToFile(requestInfo);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("product/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        private void WriteToFile(RequestInfo requestInfo)
        {
            var file = Server.MapPath("~/App_Data/RequestsInfo.txt");
            System.IO.File.AppendAllText(@file, requestInfo.ToString());
        }
    }
}
