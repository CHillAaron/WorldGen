using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldGen.Models;

namespace WorldGen.Services
{
    public class BusinessService
    {
        
        public string GetBusinessName()
        {
            string BusinessName = "The Store";
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
            string businessName = "General store";
            Business randomBusiness = new Business()
            {
                BusinessName = GetBusinessName(),
                BusinessType = GetBusinessType(),
            };
            return randomBusiness;
        }
    }
}
