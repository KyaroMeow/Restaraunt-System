using Microsoft.EntityFrameworkCore;
using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Core.Services;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IDishRepository _dishRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            ITableRepository tableRepository,
            IDishRepository dishRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _tableRepository = tableRepository;
            _dishRepository = dishRepository;
        }

        public async Task<Order> CreateOrderAsync(int tableId, int? employeeId)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);
            if (table == null)
                throw new ArgumentException("Table not found");

            var order = new Order
            {
                TableId = tableId,
                StatusId = 1,
                OrderTime = DateTime.Now
            };

            return await _orderRepository.AddAsync(order);
        }

        public async Task<Order> GetOrderDetailsAsync(int orderId)
        {
            return await _orderRepository.GetOrderWithItemsAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetActiveOrdersAsync()
        {
            return await _orderRepository.GetByStatusAsync(2);
        }

        public async Task AddItemToOrderAsync(int orderId, int dishId, int quantity)
        {
            var dish = await _dishRepository.GetByIdAsync(dishId);
            if (dish == null)
                throw new ArgumentException("Dish not found");

            var orderItem = new OrderItem
            {
                OrderId = orderId,
                DishId = dishId,
                Quantity = quantity
            };


            await _orderItemRepository.AddAsync(orderItem);

            // Обновляем статус заказа на "В работе"
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order.StatusId == 1)
            {
                order.StatusId = 2;
                await _orderRepository.UpdateAsync(order);
            }
        }

        public async Task RemoveItemFromOrderAsync(int orderId, int dishId)
        {
            await _orderItemRepository.DeleteAsync(orderId, dishId);
        }

        public async Task CompleteOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            order.StatusId = 5;
            await _orderRepository.UpdateAsync(order);
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            order.StatusId = 6;
            await _orderRepository.UpdateAsync(order);
        }


    }
}
