using Microsoft.AspNetCore.Mvc;
using TestExercise.Data.Repositories.Interfaces;
using TestExercise.Models;
using TestExercise.Models.ViewModels;

namespace TestExercise.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProviderRepository _providerRepository;
        public OrderController(IOrderRepository orderRepository, IProviderRepository providerRepository)
        {
            _orderRepository = orderRepository;
            _providerRepository = providerRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            var model = await _orderRepository.GetOrderByIdAsync(id);
            if (model != null)
            {
                return View(model);
            }
            return new NotFoundResult();
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            if (_providerRepository.Providers != null)
            {
                if (_providerRepository.Providers.Count() == 0)//Затычка для теста ( можно создать поставщика через ссылку в меню)
                {
                    await _providerRepository.AddAsync(new Provider { Name = "Test" });
                }
                var model = new CreateOrderViewModel()
                {
                    Order = new Order(),
                    Providers = _providerRepository.Providers
                };
                return View(model);
            }
            return new BadRequestResult();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        {

            if (await _orderRepository.AddOrderAsync(model.Order))
            {
                var orders = _orderRepository.Orders;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.ErrorMessage = "Номер заказа не может повторяться у одного поставщика";
                model.Providers = _providerRepository.Providers;
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order.Items == null)
            {
                order.Items = new List<OrderItem>();
            }
            var model = new CreateOrderViewModel()
            {
                Order = order,
                Providers = _providerRepository.Providers
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateOrderViewModel model)
        {
            if (await _orderRepository.UpdateOrderAsync(model.Order))
            {
                return RedirectToAction("Index", "Home");
            }
            return new NotFoundResult();
        }


        [HttpGet]
        public IActionResult AddItem(int id)
        {

            var model = new OrderItem()
            {
                OrderId = id,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(OrderItem item)
        {

            var order = await _orderRepository.GetOrderByIdAsync(item.OrderId);
            await _orderRepository.AddOrderItemAsync(item);
            await _orderRepository.UpdateOrderAsync(order);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderRepository.RemoveOrderAsync(id);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (await _orderRepository.RemoveOrderItemAsync(id))
            {
                return RedirectToAction("Index", "Home");
            }
            return new NotFoundResult();

        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _orderRepository.GetOrderItemByIdAsync(id);
            if (model != null)
            {
                return View(model);
            }
            return new NotFoundResult();
        }
    }
}
