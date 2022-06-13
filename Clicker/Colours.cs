using SQLite;

namespace Clicker
{
    [Table("colours")]
    public class Colour
    {
        [PrimaryKey]
        public int id { get; set; }
        public string NavBar { get; set; }
        public string Background { get; set; }
        public string TextColour { get; set; }
        public int Price { get; set; }
        public bool Purchased { get; set; }
    }
}