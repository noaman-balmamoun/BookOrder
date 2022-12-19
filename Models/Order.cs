using System.ComponentModel.DataAnnotations;

namespace BookOrder.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int bookId { get; set; }
        public int userid { get; set; }
        public int quantity { get; set; }
        public DateTime orderdate { get; set; }

    }
}
