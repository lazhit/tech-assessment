using Accessors.Context;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using DTOs = Accessors.DataTransferObjects;

namespace Accessors
{
    public class InventoryAccessor : IInventoryAccessor
    {
        public InventoryAccessor (
            InventoryDbContext inventoryDbContext,
            IMapper mapper)
        {
            InventoryDbContext = inventoryDbContext;
            Mapper = mapper;
        }

        private InventoryDbContext InventoryDbContext { get; set; }


        private IMapper Mapper { get; set; }

        public DTOs.Order CreateOrder (DTOs.Order order)
        {
            order.DateCreated ??= DateTime.Now;
            order.DateLastModified ??= DateTime.Now;

            var mappedEntityOrder = Mapper.Map<Entities.Order>(order);
            var orderAdded = InventoryDbContext.Orders.Add(mappedEntityOrder).Entity;
            InventoryDbContext.SaveChanges();

            return Mapper.Map<DTOs.Order>(orderAdded);
        }

        public List<DTOs.Order> GetOrdersByCustomerId(int customerId)
        {
            var entityOrders = InventoryDbContext.Orders
                .Where(order => order.CustomerId == customerId)
                .ToList();

            return Mapper.Map<List<DTOs.Order>>(entityOrders);
        }

        public string CancelOrder(int OrderId)
        {
            var entityOrder = InventoryDbContext.Orders.SingleOrDefault(order => order.Id == OrderId);

            if(entityOrder == null)
            {
                return null;
            }

            InventoryDbContext.Remove(entityOrder);
            InventoryDbContext.SaveChanges();

            return $"Order {OrderId} cancelled.";

        }

        public DTOs.Order UpdateOrder(DTOs.Order order)
        {
            var entityOrder = InventoryDbContext.Orders.SingleOrDefault(existingOrder => existingOrder.Id == order.Id);

            if (entityOrder == null)
            {
                return null;
            }

            entityOrder.Name = order.Name;
            entityOrder.DateLastModified = DateTime.Now;

            var updatedOrder = InventoryDbContext.Update(entityOrder).Entity;
            InventoryDbContext.SaveChanges();
            
            return Mapper.Map<DTOs.Order>(updatedOrder);
        }
    }

}
