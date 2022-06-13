using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clicker
{
    [Table("digits")]
    public class Digit
    {
        [PrimaryKey]
        public int id { get; set; }
        public string Nameofthedigit { get; set; }    
        public double DesiredDigit { get; set; } 

    }
}
