using AutoMapper;
using System.Collections.Generic;
using DTOs = Accessors.DataTransferObjects;
using Accessors;

namespace Managers
{
    public class InventoryManager : IInventoryManager
    {
        public InventoryManager(IInventoryAccessor inventoryAccessor, IMapper mapper)
        {
            InventoryAccessor = inventoryAccessor;
            Mapper = mapper;
        }

        private IInventoryAccessor InventoryAccessor { get; set; }

        private IMapper Mapper { get; set; }

        public Contracts.Order CreateOrder(Contracts.Order order)
        {
            var mappedDTOOrder = Mapper.Map<DTOs.Order>(order);
            var orderAdded = InventoryAccessor.CreateOrder(mappedDTOOrder);
            return Mapper.Map<Contracts.Order>(orderAdded);
        }

        public List<Contracts.Order> GetOrdersByCustomerId(int customerId)
        {
            var orders = InventoryAccessor.GetOrdersByCustomerId(customerId);
            return Mapper.Map<List<Contracts.Order>>(orders);
        }

        public string CancelOrder(int OrderId)
        {
            return InventoryAccessor.CancelOrder(OrderId);
        }

        public Contracts.Order UpdateOrder(Contracts.Order order)
        {
            var mappedDTOOrder = Mapper.Map<DTOs.Order>(order);
            var updatedOrder = InventoryAccessor.UpdateOrder(mappedDTOOrder);
            return Mapper.Map<Contracts.Order>(updatedOrder);
        }
    }
}
