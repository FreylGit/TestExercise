using Microsoft.AspNetCore.Mvc;
using TestExercise.Data.Repositories.Interfaces;
using TestExercise.Models;
using TestExercise.Models.ViewModels;

namespace TestexErcise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IProviderRepository _providerRepository;

        public HomeController(ILogger<HomeController> logger, IOrderRepository orderRepository, IProviderRepository providerRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _providerRepository = providerRepository;
        }

        public IActionResult Index()
        {
           
            DateTime startm = DateTime.Now.AddMonths(-1);
            var modelDefault = new OrderListViewModel()
            {
                Orders = _orderRepository.Orders,
                DateStart = startm,
                DateEnd = DateTime.Now,
            };
            return View(modelDefault);

        }

        [HttpPost]
        public IActionResult Filter(string date1, string date2)
        {
            DateTime start = DateTime.Parse(date1);
            DateTime end = DateTime.Parse(date2);
            var model = new OrderListViewModel()
            {
                Orders = _orderRepository.Orders.Where(o => (o.Date >= start) && (o.Date <= end)),
                DateStart = start,
                DateEnd = end,
            };
            return View("Index", model);
        }
        [HttpGet]
        public IActionResult CreateProvider()
        {
            return View(new Provider());
        }
        [HttpPost]
        public async Task<IActionResult> CreateProvider(Provider provider)
        {
           if( await _providerRepository.AddAsync(provider))
            {
                return RedirectToAction("Index");
            }
            return View(provider);
          
        }
        
    }
}