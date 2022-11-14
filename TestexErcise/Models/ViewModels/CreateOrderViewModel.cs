namespace TestExercise.Models.ViewModels
{
    public class CreateOrderViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<Provider> Providers { get; set; }
        public string ErrorMessage { get; set; }
    }
}
