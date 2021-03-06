using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using WorldGen.Models;
using WorldGen.Services;

namespace WorldGen.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly PeopleService peopleService;
        private readonly BusinessService businessService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            peopleService = new PeopleService();
            businessService = new BusinessService();
        }

        List<People> RandomGen = new List<People>();


        public IActionResult Index()
        {
            return RedirectToAction("CreatePerson");
        }

        public IActionResult CreatePerson()
        {
            return View();
        }

        [HttpPost("personCreate")]
        public IActionResult PersonCreate(int numToCreated)
        {
            try
            {
                //List<People> RandomGen = new List<People>();
                var database = "Host=192.168.1.104;Username=postgres;Password=root;Database=WorldGen";
                using var conn = new NpgsqlConnection(database);
                conn.Open();
                int i = 1;
                int charindex = 0;
                while (i <= numToCreated)
                {

                    RandomGen.Add(peopleService.CreateThePerson());
                    using (var cmd = new NpgsqlCommand("insert into full_names (first_name, last_name, gender, race) values (@first, @last, @gender, @race)", conn))
                    {
                        cmd.Parameters.AddWithValue("first", RandomGen[charindex].FirstName);
                        cmd.Parameters.AddWithValue("last", RandomGen[charindex].LastName);
                        cmd.Parameters.AddWithValue("gender", RandomGen[charindex].Gender);
                        cmd.Parameters.AddWithValue("race", RandomGen[charindex].Race);
                        cmd.ExecuteNonQuery();
                    }
                    i += 1;
                    charindex += 1;
                }
                return View("CreateBusiness", RandomGen);
                //return View("CreatedPerson", RandomGen);
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return View("CreatePerson");
            }
        }

        public IActionResult CreatedPerson()
        {
            return View();
        }

        public IActionResult CreateBusiness(List<People> RandomGen)
        {
            var rnd = new Random();
            int RandomPersonSpot = rnd.Next(RandomGen.Count);
            return View(RandomPersonSpot);
        }

        [HttpPost("businessCreate")]
        public IActionResult BusinessCreate(int numToCreated)
        {
            try
            {
                
                List<Business> RandomBiz = new List<Business>();
                int i = 1;
                //var rnd = new Random();
                String RandomPerson = "Johnny";

                while (i <= numToCreated)
                {
                    RandomBiz.Add(businessService.CreateTheBusiness());
                    i += 1;
                }

                foreach (People manu in RandomGen)
                {
                    Console.WriteLine("People are: " + manu);
                }
                return View("CreatedBusiness", RandomBiz);
            }

            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return View("businessCreate");
            }
        }
        public IActionResult CreatedBusiness()
        {
            //Console.WriteLine("************************************");
            //Console.WriteLine("This is the Stock Items: "+ );
            //Console.WriteLine("************************************");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       }
}
