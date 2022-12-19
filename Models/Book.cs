using System.ComponentModel.DataAnnotations;

namespace BookOrder.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
        public string info { get; set; }
        public int bookquantity { get; set; }
        public int price { get; set; }
        public int cataid { get; set; }
        public string author { get; set; }

    }
}
