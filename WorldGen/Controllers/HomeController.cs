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

        


        public IActionResult Index()
        {
            return RedirectToAction("CreateCivil");
        }

        public IActionResult CreateCivil()
        {
            return View();
        }
        [HttpPost("civilCreate")]
        public IActionResult CivilCreate(int BusinessToCreate, int PeopleToCreate)
        {
            try
            {
                //This is the method of creating the new people
                List<People> RandomGen = new List<People>();
                int population = peopleService.GetPopulation(PeopleToCreate);
                int i = 0;
                int charindex = 0;
                while (i <= population)
                {
                    RandomGen.Add(peopleService.CreateThePerson());
                    i += 1;
                    //charindex += 1;
                }

                //This is the method of creating the business
                List<Business> RandomBiz = new List<Business>();
                int j = 1;

                while (i <= BusinessToCreate)
                {
                    RandomBiz.Add(businessService.CreateTheBusiness());
                    j += 1;
                }
                List<List> businessAndPeople = new List<List>();
                return View("CreatedCivil", RandomBiz);
            }
            catch (IOException e)
            {
                Console.WriteLine("The Civilization could not be created:");
                Console.WriteLine(e.Message);
                return View("CreateCivil");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       }
}
