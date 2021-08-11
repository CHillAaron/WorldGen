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
            string owner;
            
            string BusinessName = "The Store " + businessType;
            return BusinessName;
        }


        public string GetBusinessType()
        {
            string BusinessType;


            var rnd = new Random();
            int businessTypeChance = rnd.Next(1, 101);
            if (businessTypeChance >= 80)
            {
                return BusinessType = "ARMOR_SHOP";
            }
            else if (businessTypeChance >= 55)
            {
                return BusinessType = "BAKERY";
            }
            else
            {
                return BusinessType = "GENERAL_STORE";
            }
        }

        public Business CreateTheBusiness()
        {
            string BusinessType = GetBusinessType();
            string BusinessName = GetBusinessName(BusinessType);

            Business randomBusiness = new Business()
            {
                BusinessName = BusinessName,
                BusinessType = BusinessType
            };
            return randomBusiness;
        }
        
    }
}
