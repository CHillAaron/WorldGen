using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WorldGen.Models;

namespace WorldGen.Services
{
    public class PeopleService
    {
        string maleName = @"wwwroot/charInfo/maleNames.txt";
        string femaleName = @"wwwroot/charInfo/maleNames.txt";
        string lastName = @"wwwroot/charInfo/lastNames.txt";


        //var connString = "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase";



        List<string> firstNameList = new List<string>();
        List<string> lastNameList = new List<string>();
        

        //Gets the gender of the Person
        public string GetGender()
        {
            string gender;
            Random rnd = new Random();
            int genderChance = rnd.Next(1, 101);
            if (genderChance >= 40)
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
            int firstId;
            var rnd = new Random();
            if (gender == "female")
            {
                firstId = rnd.Next(1, 1023);
            }
            else
            {
                firstId = rnd.Next(1, 1144);
            }
            var database = "Host=192.168.1.104;Username=postgres;Password=root;Database=WorldGen";
            using var conn = new NpgsqlConnection(database);
            conn.Open();
            if (gender == "male")
            {
                //    using (var sr = new StreamReader(maleName))

                //        while (sr.Peek() >= 0)
                //        {
                //            firstNameList.Add(sr.ReadLine());
                //        }
                using (var cmd = new NpgsqlCommand($"SELECT* FROM  male_names where id = {firstId}", conn))
                {
                    //Console.WriteLine("This is the npgsqlCommand count: " + conn.Database);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    string firstName = dr[0].ToString();
                     return firstName;
                }
            }
            else
            {
                //using (var sr = new StreamReader(femaleName))

                //    while (sr.Peek() >= 0)
                //    {
                //        firstNameList.Add(sr.ReadLine());
                //    }
                using (var cmd = new NpgsqlCommand($"SELECT* FROM  female_names where id = {firstId}", conn))
                {
                    //Console.WriteLine("This is the npgsqlCommand count: " + conn.Database);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    string firstName = dr[0].ToString();
                    return firstName;
                }
            }
        }

        //Gets Last name
        public string GetLastName()
        {
            var rnd = new Random();
            int lastId = rnd.Next(1, 578);
            var database = "Host=192.168.1.104;Username=postgres;Password=root;Database=WorldGen";
            using var conn = new NpgsqlConnection(database);
            conn.Open();
            using (var cmd = new NpgsqlCommand($"SELECT* FROM  last_names where id = {lastId}", conn))
            {
                //Console.WriteLine("This is the npgsqlCommand count: " + conn.Database);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                string lastName = dr[0].ToString();
                return lastName;
            }
        }
        //Get Race
        public string GetRace() 
        {
            string Race;
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
        void addToDatabase(string gender, string firstName, string lastName, string race)
        {
            var database = "Host=192.168.1.104;Username=postgres;Password=root;Database=WorldGen";
            using var conn = new NpgsqlConnection(database);
            conn.Open();
            using (var cmd = new NpgsqlCommand("insert into full_names (first_name, last_name, gender, race) values (@first, @last, @gender, @race)", conn))
            {
                cmd.Parameters.AddWithValue("first", firstName);
                cmd.Parameters.AddWithValue("last", lastName);
                cmd.Parameters.AddWithValue("gender", gender);
                cmd.Parameters.AddWithValue("race", race);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public int GetPopulation(int PeopleToCreate)
        {
            int min = 0;
            int max = 0;
            switch (PeopleToCreate)
            {
                case (1):
                    min = 42;
                    max = 63;
                    break;
                case (2):
                    min = 187;
                    max = 213;
                    break;
                case (3):
                    min = 578;
                    max = 608;
                    break;
                case (4):
                    min = 1142;
                    max = 1290;
                    break;
            }
            var rnd = new Random();
            int population = rnd.Next(min, max);
            return population;
        }

        public People CreateThePerson()
        {
            string gender = GetGender();
            string FirstName = GetFirstName(gender);
            string LastName = GetLastName();
            string race = GetRace();
            //addToDatabase(gender, FirstName, LastName, race);

            People randomName = new People()
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
