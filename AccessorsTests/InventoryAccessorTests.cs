using Accessors;
using Accessors.Context;
using Accessors.Mapping;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using DTOs = Accessors.DataTransferObjects;
using Entities = Accessors.Entities;

namespace AccessorsTests
{
    public class InventoryAccessorTests
    {
        public InventoryAccessorTests()
        {
            var mapperConfiguration =
                new MapperConfiguration(config =>
                {
                    config.AddProfile(typeof(OrderMappingProfile));
                    config.AddProfile(typeof(CustomerMappingProfile));
                });

            Mapper = mapperConfiguration.CreateMapper();
        }

        private IMapper Mapper { get; set; }


        private readonly DbContextOptions<InventoryDbContext> inventoryInMemoryOptions =
            new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "InventoryDatabase")
                .Options;

        private InventoryDbContext InventoryDbContext => new InventoryDbContext(inventoryInMemoryOptions);

        [Fact]
        public void InventoryAccessor_CreateOrder_ShouldSucceed()
        {
            // Arrange
            using var inventoryDbContext = InventoryDbContext;

            try
            {
                // Insert seed data into the database using one instance of the context
                AddCustomers(inventoryDbContext);

                var order = new DTOs.Order
                {
                    Name = "French fries",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    CustomerId = 1
                };

                var inventoryAccessor = new InventoryAccessor(inventoryDbContext, Mapper);

                // Act
                var orderCreated = inventoryAccessor.CreateOrder(order);

                // Assert
                Assert.NotNull(orderCreated);
                Assert.Equal(order.Name, orderCreated.Name);
            }
            finally
            {
                inventoryDbContext.ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(e => e.State = EntityState.Detached);
                inventoryDbContext.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void InventoryAccessor_GetOrdersByCustomerId_ShouldSucceed()
        {
            // Arrange
            using var inventoryDbContext = InventoryDbContext;

            try
            {
                // Insert seed data into the database using one instance of the context
                AddCustomers(inventoryDbContext);
                AddOrders(inventoryDbContext);

                var inventoryAccessor = new InventoryAccessor(inventoryDbContext, Mapper);

                // Act
                var orders = inventoryAccessor.GetOrdersByCustomerId(1);

                // Assert
                Assert.True(orders.Any());
                Assert.Equal(2, orders.Count);
            }
            finally
            {
                inventoryDbContext.ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(e => e.State = EntityState.Detached);
                inventoryDbContext.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void InventoryAccessor_CancelOrder_ForNotExistingOrder_ShouldReturnNull()
        {
            // Arrange
            using var inventoryDbContext = InventoryDbContext;

            try
            {
                var inventoryAccessor = new InventoryAccessor(inventoryDbContext, Mapper);

                // Act
                var result = inventoryAccessor.CancelOrder(99);

                // Assert
                Assert.Null(result);
            }
            finally
            {
                inventoryDbContext.ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(e => e.State = EntityState.Detached);
                inventoryDbContext.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void InventoryAccessor_CancelOrder_ForExistingOrder_ShouldSucceed()
        {
            // Arrange
            using var inventoryDbContext = InventoryDbContext;

            try
            {
                const int orderId = 2;

                // Insert seed data into the database using one instance of the context
                AddCustomers(inventoryDbContext);
                AddOrders(inventoryDbContext);

                var inventoryAccessor = new InventoryAccessor(inventoryDbContext, Mapper);

                // Act
                var result = inventoryAccessor.CancelOrder(orderId);

                // Assert
                Assert.Equal($"Order {orderId} cancelled.", result);
            }
            finally
            {
                inventoryDbContext.ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(e => e.State = EntityState.Detached);
                inventoryDbContext.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void InventoryAccessor_UpdateOrder_ForNotExistingOrder_ShouldReturnNull()
        {
            // Arrange
            using var inventoryDbContext = InventoryDbContext;

            try
            {
                // Insert seed data into the database using one instance of the context
                AddCustomers(inventoryDbContext);

                var order = new DTOs.Order
                {
                    Id = 1,
                    Name = "Hashbrowns",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    CustomerId = 1
                };

                var inventoryAccessor = new InventoryAccessor(inventoryDbContext, Mapper);

                // Act
                var orderUpdated = inventoryAccessor.UpdateOrder(order);

                // Assert
                Assert.Null(orderUpdated);
            }
            finally
            {
                inventoryDbContext.ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(e => e.State = EntityState.Detached);
                inventoryDbContext.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void InventoryAccessor_UpdateOrder_ForExistingOrder_ShouldSucceed()
        {
            // Arrange
            using var inventoryDbContext = InventoryDbContext;

            try
            {
                // Insert seed data into the database using one instance of the context
                AddCustomers(inventoryDbContext);
                AddOrders(inventoryDbContext);

                var order = new DTOs.Order
                {
                    Id = 1,
                    Name = "Hashbrowns",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    CustomerId = 1
                };

                var inventoryAccessor = new InventoryAccessor(inventoryDbContext, Mapper);

                // Act
                var orderUpdated = inventoryAccessor.UpdateOrder(order);

                // Assert
                Assert.NotNull(orderUpdated);
                Assert.Equal(order.Name, orderUpdated.Name);
            }
            finally
            {
                inventoryDbContext.ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(e => e.State = EntityState.Detached);
                inventoryDbContext.Database.EnsureDeleted();
            }
        }

        private void AddCustomers(InventoryDbContext context)
        {
            context.Customers.AddRange(
                new List<Entities.Customer>
                {
                    new Entities.Customer
                    {
                        Id = 1,
                        Name = "Karen"
                    },
                    new Entities.Customer
                    {
                        Id = 2,
                        Name = "Ama"
                    },
                    new Entities.Customer
                    {
                        Id = 3,
                        Name = "Tsuku"
                    }
                });

            context.SaveChanges();
        }

        private void AddOrders(InventoryDbContext context)
        {
            context.Orders.AddRange(
                new List<Entities.Order>
                {
                    new Entities.Order
                    {
                        Id = 1,
                        Name = "French fries",
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now,
                        CustomerId = 1
                    },
                    new Entities.Order
                    {
                        Id = 2,
                        Name = "Mashed potatoes",
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now,
                        CustomerId = 1
                    },
                    new Entities.Order
                    {
                        Id = 3,
                        Name = "Scratching post",
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now,
                        CustomerId = 2
                    }
                });

            context.SaveChanges();
        }
    }
}
