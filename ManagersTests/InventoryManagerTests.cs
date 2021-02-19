using Accessors;
using AutoMapper;
using Managers;
using Managers.Contracts;
using Moq;
using System.Collections.Generic;
using Xunit;
using DTOs = Accessors.DataTransferObjects;

namespace ManagersTests
{
    public class InventoryManagerTests
    {
        private Mock<IMapper> MockMapper
        {
            get
            {
                var mockMapper = new Mock<IMapper>(MockBehavior.Strict);
                
                mockMapper
                    .Setup(mapper => mapper.Map<DTOs.Order>(It.IsAny<Order>()))
                    .Returns(new DTOs.Order());
                mockMapper
                    .Setup(mapper => mapper.Map<Order>(It.IsAny<DTOs.Order>()))
                    .Returns(new Order());
                mockMapper
                    .Setup(mapper => mapper.Map<List<DTOs.Order>>(It.IsAny<List<Order>>()))
                    .Returns(new List<DTOs.Order>());
                mockMapper
                    .Setup(mapper => mapper.Map<List<Order>>(It.IsAny<List<DTOs.Order>>()))
                    .Returns(new List<Order>());

                return mockMapper;
            }
        }

        private Mock<IInventoryAccessor> MockInventoryAccessor
        {
            get
            {
                var mockInventoryAccessor = new Mock<IInventoryAccessor>(MockBehavior.Strict);

                mockInventoryAccessor
                    .Setup(inventoryAccessor => inventoryAccessor.CreateOrder(It.IsAny<DTOs.Order>()))
                    .Returns(new DTOs.Order());
                mockInventoryAccessor
                    .Setup(inventoryAccessor => inventoryAccessor.GetOrdersByCustomerId(It.IsAny<int>()))
                    .Returns(new List<DTOs.Order>());
                mockInventoryAccessor
                    .Setup(inventoryAccessor => inventoryAccessor.CancelOrder(It.IsAny<int>()))
                    .Returns(string.Empty);
                mockInventoryAccessor
                    .Setup(inventoryAccessor => inventoryAccessor.UpdateOrder(It.IsAny<DTOs.Order>()))
                    .Returns(new DTOs.Order());

                return mockInventoryAccessor;
            }
        }

        [Fact]
        public void InventoryManager_CreateOrder_ShouldReturnCreatedOrder()
        {
            // Arrange
            var inventoryManager = new InventoryManager(MockInventoryAccessor.Object, MockMapper.Object);

            // Act
            var createdOrder = inventoryManager.CreateOrder(new Order());

            // Assert
            Assert.NotNull(createdOrder);
        }

        [Fact]
        public void InventoryManager_GetOrdersByCustomerId_ShouldReturnOrders()
        {
            // Arrange
            var inventoryManager = new InventoryManager(MockInventoryAccessor.Object, MockMapper.Object);

            // Act
            var orders = inventoryManager.GetOrdersByCustomerId(1);

            // Assert
            Assert.NotNull(orders);
        }

        [Fact]
        public void InventoryManager_CancelOrder_ShouldReturnOrders()
        {
            // Arrange
            var inventoryManager = new InventoryManager(MockInventoryAccessor.Object, MockMapper.Object);

            // Act
            var message = inventoryManager.CancelOrder(1);

            // Assert
            Assert.Equal(string.Empty, message);
        }

        [Fact]
        public void InventoryManager_UpdateOrder_ShouldReturnOrders()
        {
            // Arrange
            var inventoryManager = new InventoryManager(MockInventoryAccessor.Object, MockMapper.Object);

            // Act
            var updatedOrder = inventoryManager.UpdateOrder(new Order());

            // Assert
            Assert.NotNull(updatedOrder);
        }
    }
}
