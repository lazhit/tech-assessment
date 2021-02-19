using CSharp.Controllers;
using Managers;
using Managers.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ControllerTests
{
    public class OrderControllerTests
    {
        private Order PopulatedOrder
        {
            get
            {
                return new Order
                {
                    Id = 1,
                    Name = "Taco",
                    CustomerId = 1
                };
            }
        }

        [Fact]
        public void OrderController_CreateOrder_WithNullOrder_ShouldReturnBadRequest()
        {
            // Arrange
            var orderController = new OrderController(null);

            // Act
            var actionResult = orderController.CreateOrder(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_CreateOrder_WithPopulatedOrder_ShouldReturnOk()
        {
            // Arrange
            var mockInventoryManager = new Mock<IInventoryManager>(MockBehavior.Strict);
            mockInventoryManager
                .Setup(inventoryManager => inventoryManager.CreateOrder(It.IsAny<Order>()))
                .Returns(new Order());

            var orderController = new OrderController(mockInventoryManager.Object);

            // Act
            var actionResult = orderController.CreateOrder(PopulatedOrder);

            // Assert
            Assert.IsType<CreatedResult>(actionResult);
        }

        [Fact]
        public void OrderController_CreateOrder_ThrowsException_ShouldReturnBadRequest()
        {
            // Arrange
            var exception = new Exception("This blew up. Boom.");

            var mockInventoryManager = new Mock<IInventoryManager>(MockBehavior.Strict);
            mockInventoryManager
                .Setup(inventoryManager => inventoryManager.CreateOrder(It.IsAny<Order>()))
                .Throws(exception);

            var orderController = new OrderController(mockInventoryManager.Object);

            // Act
            var actionResult = orderController.CreateOrder(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_GetOrdersByCustomerId_WithNullCustomerId_ShouldReturnBadRequest()
        {
            // Arrange
            var orderController = new OrderController(null);

            // Act
            var actionResult = orderController.GetOrdersByCustomerId(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_GetOrdersByCustomerId_WithCustomerId_ShouldReturnOk()
        {
            // Arrange
            var mockInventoryManager = new Mock<IInventoryManager>(MockBehavior.Strict);
            mockInventoryManager
                .Setup(inventoryManager => inventoryManager.GetOrdersByCustomerId(It.IsAny<int>()))
                .Returns(new List<Order>());

            var orderController = new OrderController(mockInventoryManager.Object);

            // Act
            var actionResult = orderController.GetOrdersByCustomerId(1);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_GetOrdersByCustomerId_ThrowsException_ShouldReturnBadRequest()
        {
            // Arrange
            var exception = new Exception("This blew up. Boom.");

            var mockInventoryManager = new Mock<IInventoryManager>(MockBehavior.Strict);
            mockInventoryManager
                .Setup(inventoryManager => inventoryManager.GetOrdersByCustomerId(It.IsAny<int>()))
                .Throws(exception);

            var orderController = new OrderController(mockInventoryManager.Object);

            // Act
            var actionResult = orderController.GetOrdersByCustomerId(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_CancelOrder_WithNullOrderId_ShouldReturnBadRequest()
        {
            // Arrange
            var orderController = new OrderController(null);

            // Act
            var actionResult = orderController.CancelOrder(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_CancelOrder_WithOrderId_FoundOrder_ShouldReturnOk()
        {
            // Arrange
            var mockInventoryManager = new Mock<IInventoryManager>(MockBehavior.Strict);
            mockInventoryManager
                .Setup(inventoryManager => inventoryManager.CancelOrder(It.IsAny<int>()))
                .Returns(string.Empty);

            var orderController = new OrderController(mockInventoryManager.Object);

            // Act
            var actionResult = orderController.CancelOrder(1);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_CancelOrder_WithOrderId_NotFoundOrder_ShouldReturnNotFound()
        {
            // Arrange
            var mockInventoryManager = new Mock<IInventoryManager>(MockBehavior.Strict);
            mockInventoryManager
                .Setup(inventoryManager => inventoryManager.CancelOrder(It.IsAny<int>()))
                .Returns(null as string);

            var orderController = new OrderController(mockInventoryManager.Object);

            // Act
            var actionResult = orderController.CancelOrder(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_CancelOrder_ThrowsException_ShouldReturnBadRequest()
        {
            // Arrange
            var exception = new Exception("This blew up. Boom.");

            var mockInventoryManager = new Mock<IInventoryManager>(MockBehavior.Strict);
            mockInventoryManager
                .Setup(inventoryManager => inventoryManager.CancelOrder(It.IsAny<int>()))
                .Throws(exception);

            var orderController = new OrderController(mockInventoryManager.Object);

            // Act
            var actionResult = orderController.CancelOrder(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_UpdateOrder_WithNullOrder_ShouldReturnBadRequest()
        {
            // Arrange
            var orderController = new OrderController(null);

            // Act
            var actionResult = orderController.UpdateOrder(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_UpdateOrder_WithNullOrderId_ShouldReturnBadRequest()
        {
            // Arrange
            PopulatedOrder.Id = 0;
            var orderController = new OrderController(null);

            // Act
            var actionResult = orderController.UpdateOrder(PopulatedOrder);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_UpdateOrder_WithOrderId_FoundOrder_ShouldReturnOk()
        {
            // Arrange
            var mockInventoryManager = new Mock<IInventoryManager>(MockBehavior.Strict);
            mockInventoryManager
                .Setup(inventoryManager => inventoryManager.UpdateOrder(It.IsAny<Order>()))
                .Returns(new Order());

            var orderController = new OrderController(mockInventoryManager.Object);

            // Act
            var actionResult = orderController.UpdateOrder(PopulatedOrder);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_UpdateOrder_WithOrderId_NotFoundOrder_ShouldReturnNotFound()
        {
            // Arrange
            var mockInventoryManager = new Mock<IInventoryManager>(MockBehavior.Strict);
            mockInventoryManager
                .Setup(inventoryManager => inventoryManager.UpdateOrder(It.IsAny<Order>()))
                .Returns(null as Order);

            var orderController = new OrderController(mockInventoryManager.Object);

            // Act
            var actionResult = orderController.UpdateOrder(PopulatedOrder);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public void OrderController_UpdateOrder_ThrowsException_ShouldReturnBadRequest()
        {
            // Arrange
            var exception = new Exception("This blew up. Boom.");

            var mockInventoryManager = new Mock<IInventoryManager>(MockBehavior.Strict);
            mockInventoryManager
                .Setup(inventoryManager => inventoryManager.UpdateOrder(It.IsAny<Order>()))
                .Throws(exception);

            var orderController = new OrderController(mockInventoryManager.Object);

            // Act
            var actionResult = orderController.UpdateOrder(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }
    }
}
