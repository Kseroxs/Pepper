using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pepper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Pepper.Controllers
{
    public class HomeController : Controller
    {
        private readonly PepperDBContext context;
        public HomeController()
        {
            context = new PepperDBContext();
        }
        private static List<Promocje> _promocja = new List<Promocje>();
        
        //public async Task <IActionResult> Index()
        //{
        //    //var apiKey = "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";
        //    //var client = new SendGridClient(apiKey);
        //    //var from = new EmailAddress("erbopl@gmail.com", "Szymon Lorek");
        //    //var subject = "Sending with SendGrid is Fun";
        //    //var to = new EmailAddress("szejo-slu@o2.pl", "Szymon Tengler");
        //    //var plainTextContent = "and easy to do anywhere, even with C#";
        //    //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        //    //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    //var response = await client.SendEmailAsync(msg);
        //    return View(_promocja);
        //}
    public IActionResult Index()
        {
            _promocja = context.Promocje.ToList();
            

            return View(_promocja);
        }
        [HttpGet]
        public IActionResult DodajPromocje()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DodajPromocje(Promocje Promocje)
        {
            //if (ModelState.IsValid)
            //{
            //    context.Promocje.Add(Promocje);
            //    return RedirectToAction(nameof(Index));
            //}
            DateTime dzis = DateTime.Now;
            Promocje prom = new Promocje();
            prom.Nazwa = Promocje.Nazwa;
            prom.Opis = Promocje.Opis;
            prom.Link = Promocje.Link;
            dzis = prom.DataDodania;

            context.Promocje.Add(Promocje);
            context.SaveChanges();


            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("erbopl@gmail.com", "Szymon Lorek");
            var subject = "Nowa promocja";
            var to = new EmailAddress(Promocje.Email);
            var plainTextContent = "Na stronie pojawiła się Twoja nowa promocja";
            var htmlContent = "Na stronie pojawiła się Twoja nowa promocja, dotyczy ona " + prom.Nazwa;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Sent()
        {
            return View();
        }
        public ActionResult Logowanie()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
