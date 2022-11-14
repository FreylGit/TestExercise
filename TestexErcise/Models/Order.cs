using System.ComponentModel.DataAnnotations;

namespace TestExercise.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Number { get;set; }
        public DateTime Date { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
        public IEnumerable<OrderItem>? Items { get; set; }
        
    }
}
