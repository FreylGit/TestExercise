namespace TestExercise.Models.ViewModels
{
    public class OrderListViewModel
    {
        public IEnumerable<Order>Orders { get; set; }
        public DateTime DateStart { get; set; } 
        public DateTime DateEnd { get; set; }
        public string Filter { get; set; }
    }
}
