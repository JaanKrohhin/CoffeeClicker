using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clicker
{
    [Table("upgrades")]
    public class Upgrade
    {
        [PrimaryKey]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public double Value { get; set; }
        public string Picture { get; set; }
        public int OneTimePurchase { get; set; }//0 = not a one time, 1 is one time but not bought, 2 is purchased one time upgrade
    }
}
