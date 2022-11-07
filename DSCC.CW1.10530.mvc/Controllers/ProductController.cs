using DSCC.CW1._10530.mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DSCC.CW1._10530.mvc.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<ActionResult> Index()
        {
            string Baseurl = "https://localhost:44346/";
            List<Product> ProdInfo = new List<Product>();
            using (var client = new HttpClient())
            {
                //passing service base url
                client.BaseAddress = new Uri(Baseurl);      
                client.DefaultRequestHeaders.Clear();

                //request data format
                client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("application/json"));

                //send request to find web api REST service resource
                HttpResponseMessage Res = await client.GetAsync("api/Product");

                //check response
                if (Res.IsSuccessStatusCode)
                {
                    //storing data
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //deserializing response
                    ProdInfo = JsonConvert.DeserializeObject<List<Product>>(PrResponse);
                }
                //return the Product list to view
                return View(ProdInfo);
            }
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string Baseurl = "https://localhost:44346/";
            Product product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("api/Product/" + id);
                //Checking the response is successful or not which is sent usingHttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    product = JsonConvert.DeserializeObject<Product>(PrResponse);
                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }

            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<ActionResult> Create(Product prod)
        {
            try
            {
                // TODO: Add update logic here
                string Baseurl = "https://localhost:44346/";
                using (var client = new HttpClient())
                {
                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Product>("api/Product/",prod);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.")
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string Baseurl = "https://localhost:44346/";
            Product product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("api/Product/" + id);
                //Checking the response is successful or not which is sent usingHttpClient
            if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    product = JsonConvert.DeserializeObject<Product>(PrResponse);
                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Product prod)
        {
            try
            {
                // TODO: Add update logic here
                string Baseurl = "https://localhost:44346/";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    HttpResponseMessage Res = await client.GetAsync("api/Product/" + id);
                    Product product = null;
                    //Checking the response is successful or not which is sent usingHttpClient
                if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api 
                        var PrResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storinginto the Product list
                        product = JsonConvert.DeserializeObject<Product>(PrResponse);
                    }
                    prod.ProductCategory = product.ProductCategory;
                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<Product>("api/Product/" + prod.Id,
                    prod);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                //ModelState.AddModelError(string.Empty, "Server Error. Please contactadministrator.");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            string Baseurl = "https://localhost:44346/";
            Product product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("api/Product/" + id);
                //Checking the response is successful or not which is sent usingHttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    product = JsonConvert.DeserializeObject<Product>(PrResponse);
                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
