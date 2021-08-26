using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldGen.Models;
using WorldGen.Services;

namespace WorldGen.Services
{
    public class BusinessService
    {       
        string BusinessName;
        public string GetBusinessName(string businessType)
        {
            var database = "Host=192.168.1.104;Username=postgres;Password=root;Database=WorldGen";
            using var conn = new NpgsqlConnection(database);
            conn.Open();
            string owner;
            //------------------- sets the max number of the People that have already been created ----------------------------
            var peopleCountCreate = new NpgsqlCommand($"SELECT COUNT(*) FROM full_names", conn);
                Int64 peopleCreated = (Int64)peopleCountCreate.ExecuteScalar();
                int personCount = (int)peopleCreated;
            //------------------------- Sets a random number for the id to select the created person for the Owner. ----------------------------
            var rnd = new Random();
            int id = rnd.Next(1, personCount);
            //--------- sets the name for the Shop. ---------------------------------
            using (var cmd = new NpgsqlCommand($"SELECT* FROM  full_names where full_name_id = {id}", conn))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                owner = dr[0].ToString() + " " + dr[1].ToString();
            }
            //-------------- sets the full business name ---------------------
            conn.Close();
            string BusinessName = $"{owner}'s {businessType}";
            return BusinessName;
        }

        public string GetBusinessType()
        {
            string BusinessType;
            var rnd = new Random();
            int businessTypeChance = rnd.Next(1, 101);
            if (businessTypeChance >= 80)
            {
                return BusinessType = "ARMOR";
            }
            else if (businessTypeChance >= 55)
            {
                return BusinessType = "Weapon";
            }
            else
            {
                return BusinessType = "CLOTHES";
            }
        }

        public List<Dictionary<string, string>> GetIventory(string BusinessType)
        {

            ////opens the connection
            //var database = "Host=192.168.1.104;Username=postgres;Password=root;Database=WorldGen";
            //using var conn = new NpgsqlConnection(database);
            //conn.Open();
            ////Creates the count of how many items of that type there are and shrinks it down to an integer to be used.
            //var inventoryCreate = new NpgsqlCommand($"SELECT COUNT(*) FROM inventory where item_type = 'TOOL'", conn);
            //Int64 iventoryCreated = (Int64)inventoryCreate.ExecuteScalar();
            //int inventoryAmount = (int)iventoryCreated;
            int inventoryAmount = GetIventoryCount();
            //selects a random number from 1 and the amount of the item type to give to the shop
            var rnd = new Random();
            int inventoryStock = rnd.Next(1, inventoryAmount);
            
            //list of dictionary that holds the the stock of the store.
            List<Dictionary<string, string>> listOfTools = new List<Dictionary<string, string>>();

            //opens the connection
            var database = "Host=192.168.1.104;Username=postgres;Password=root;Database=WorldGen";
            using var conn = new NpgsqlConnection(database);
            conn.Open();

            int i = 0;

                using (var cmd = new NpgsqlCommand($"SELECT * FROM inventory where item_type = 'TOOL'", conn))
                {
                    NpgsqlDataReader dr = cmd.ExecuteReader();
            while (i < inventoryStock)
            {
                    dr.Read();
                    listOfTools.Add(new Dictionary<string, string> { { "item_name", dr[0].ToString() }, { "item_type", dr[1].ToString() }, { "item_price", dr[2].ToString() }, { "money_type", dr[3].ToString() } });
                    i++;
                }
            }
                return listOfTools;
        }

        public int GetIventoryCount()
        {
            //opens the connection
            var database = "Host=192.168.1.104;Username=postgres;Password=root;Database=WorldGen";
            using var conn = new NpgsqlConnection(database);
            conn.Open();
            //Creates the count of how many items of that type there are and shrinks it down to an integer to be used.
            var inventoryCreate = new NpgsqlCommand($"SELECT COUNT(*) FROM inventory where item_type = 'TOOL'", conn);
            Int64 iventoryCreated = (Int64)inventoryCreate.ExecuteScalar();
            int inventoryAmount = (int)iventoryCreated;
            conn.Close();
            return inventoryAmount;
        }

        public Business CreateTheBusiness()
        {
            string BusinessType = GetBusinessType();
            string BusinessName = GetBusinessName(BusinessType);
            //string InventoryList = GetIventory(BusinessType);
            List<Dictionary<string, string>> InventoryList = GetIventory(BusinessType);


            Business randomBusiness = new Business()
            {
                BusinessName = BusinessName,
                BusinessType = BusinessType,
                InventoryList = InventoryList
            };
            return randomBusiness;
        }
        
    }
}
