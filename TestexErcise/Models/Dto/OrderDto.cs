using System.ComponentModel.DataAnnotations;

namespace TestExercise.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public ProviderDto Provider { get; set; }
        public int ProviderId { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }
    }
}
