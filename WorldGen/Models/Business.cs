using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldGen.Models
{
    public class Business
    {
        public int ID { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public List<Dictionary<string, string>> InventoryList { get; set; }
        //public string InventoryList { get; set; }
    }
}
