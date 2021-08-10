using System;
using System.Collections.Generic;
using System.IO;
using WorldGen.Models;

namespace WorldGen.Services
{
    public class PeopleService
    {
        string maleName = @"wwwroot/charInfo/maleNames.txt";
        string femaleName = @"wwwroot/charInfo/maleNames.txt";
        string lastName = @"wwwroot/charInfo/lastNames.txt";

        List<string> firstNameList = new List<string>();
        List<string> lastNameList = new List<string>();
        string FirstName;
        string LastName;
        string FullName;
        string Race;

        //Gets the gender of the Person
        public string GetGender()
        {
            string gender;
            Random rnd = new Random();
            int genderChance = rnd.Next(1, 101);
            if (genderChance >= 30)
            {
                return gender = "female";
            }
            else
            {
                return gender = "male";
            }
        }

        public String GetFirstName(string gender)
        {
            if (gender == "male")
            {
                using (var sr = new StreamReader(maleName))

                    while (sr.Peek() >= 0)
                    {
                        firstNameList.Add(sr.ReadLine());
                    }
            }
            else
            {
                using (var sr = new StreamReader(femaleName))

                    while (sr.Peek() >= 0)
                    {
                        firstNameList.Add(sr.ReadLine());
                    }
            }

            var rnd = new Random();
            int first = rnd.Next(firstNameList.Count);
            string FirstName = firstNameList[first];

            return FirstName;
        }

        //Gets Last name
        public string GetLastName()
        {
            var rnd = new Random();
            using (var sr = new StreamReader(lastName))
                while (sr.Peek() >= 0)
                {
                    lastNameList.Add(sr.ReadLine());
                }
            int last = rnd.Next(lastNameList.Count);
            string LastName = firstNameList[last];
            return LastName;
        }
        //Get Race
        public string GetRace()
        {
            var rnd = new Random();
            int raceChance = rnd.Next(1, 101);
            if (raceChance >= 80)
            {
                return Race = "Elf";
            }
            else if (raceChance >= 55)
            {
                return Race = "Orc";
            }
            else
            {
                return Race = "Human";
            }
        }

        public People CreateThePerson()
        {
            string gender = GetGender();
            string FirstName = GetFirstName(gender);
            string LastName = GetLastName();
            string race = GetRace();

            People randomName = new People()
            //string[] randomName = new string[]
            {
                FirstName = FirstName,
                LastName = LastName,
                Gender = gender,
                Race = race
            };

            return randomName;
        }
    }
}
