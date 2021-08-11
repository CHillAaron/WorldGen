﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            return RedirectToAction("CreatePerson");
        }

        public IActionResult CreatePerson()
        {
            return View();
        }

        [HttpPost("personCreate")]
        public IActionResult PersonCreate(int numToCreated, List<People> RandomGen)
        {
            try
            {
                //List<People> RandomGen = new List<People>();
                int i = 1;
                while (i <= numToCreated)
                {
                    RandomGen.Add(peopleService.CreateThePerson());
                    i += 1;
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
            return View();
        }

        [HttpPost("businessCreate")]
        public IActionResult BusinessCreate(int numToCreated, List<People> RandomGen)
        {
            try
            {
                
                List<Business> RandomBiz = new List<Business>();
                int i = 1;
                var rnd = new Random();
                int RandomPersonSpot = rnd.Next(RandomGen.Count);
                Console.WriteLine("This is the RandomPersonSpot: " + RandomPersonSpot);
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       }
}
