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
            var orders = _orderRepository.Orders;
            if (orders == null)
            {
                var modelDefault = new OrderListViewModel()
                {
                    Orders = new List<Order>(),
                    DateStart = startm,
                    DateEnd = DateTime.Now,
                };
                return View(modelDefault);
            }
            else
            {
                var modelDefault = new OrderListViewModel()
                {
                    Orders = orders,
                    DateStart = startm,
                    DateEnd = DateTime.Now,
                };
                return View(modelDefault);
            }
        }
        /// <summary>
        ///  Метод для сортировки списка
        /// </summary>

        /// <returns></returns>
        [HttpPost]
        public IActionResult Filter(string date1, string date2, string filter)
        {
            DateTime start = DateTime.Parse(date1);
            DateTime end = DateTime.Parse(date2);
            //Костыли
            if (filter == "number")
            {
                var orders = _orderRepository.Orders.Where(o => (o.Date >= start) && (o.Date <= end))
                               .OrderBy(o => Convert.ToInt32(o.Number));

                var model = new OrderListViewModel()
                {
                    Orders = orders,
                    DateStart = start,
                    DateEnd = end,
                };
                return View("Index", model);
            }
            else if (filter == "date")
            {
                var orders = _orderRepository.Orders.Where(o => (o.Date >= start) && (o.Date <= end))
                              .OrderBy(o => o.Date);

                var model = new OrderListViewModel()
                {
                    Orders = orders,
                    DateStart = start,
                    DateEnd = end,
                };
                return View("Index", model);
            }
            else if (filter == "amount")
            {
                var orders = _orderRepository.Orders.Where(o => (o.Date >= start) && (o.Date <= end))
                              .OrderByDescending(o => o.Items.Count());

                var model = new OrderListViewModel()
                {
                    Orders = orders,
                    DateStart = start,
                    DateEnd = end,
                };
                return View("Index", model);
            }
            var ordersDefult = _orderRepository.Orders.Where(o => (o.Date >= start) && (o.Date <= end));


            var modelDefult = new OrderListViewModel()
            {
                Orders = ordersDefult,
                DateStart = start,
                DateEnd = end,
            };
            return View("Index", modelDefult);
        }
        [HttpGet]
        public IActionResult CreateProvider()
        {
            return View(new Provider());
        }
        [HttpPost]
        public async Task<IActionResult> CreateProvider(Provider provider)
        {
            if (await _providerRepository.AddAsync(provider))
            {
                return RedirectToAction("Index");
            }
            return View(provider);

        }

    }
}