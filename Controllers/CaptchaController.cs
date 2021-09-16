using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace captcha.Controllers
{
    public class CaptchaController : Controller
    {
        // GET: Captcha
       public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
       public ActionResult FormSubmit()
        {
            var response = Request["g-recaptcha-response"];
            string secretKey = "6LfuqE0cAAAAADhGOOb4lQ00RfdwUWcnrW9cXK9L";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",secretKey,response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "Google recaptcha validation success" : "Google recaptcha validation faild";
            return View("Index");
        } 
    }
}