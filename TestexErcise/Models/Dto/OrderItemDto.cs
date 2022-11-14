using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestExercise.Models.Dto
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public OrderDto Order { get; set; }
        public int OrderId { get; set; }
    }
}
