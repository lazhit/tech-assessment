using Managers;
using Managers.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CSharp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{ 
		public OrderController (IInventoryManager inventoryManager)
        {
			InventoryManager = inventoryManager;
		}

		private IInventoryManager InventoryManager { get; set; }

		/// <summary>
		/// Creates order from given order parameter.
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult CreateOrder([FromBody] Order order)
		{
			try
			{
				if (order == null)
				{
					return BadRequest("Order cannot be null.");
				}

				var orderCreated = InventoryManager.CreateOrder(order);
				return Created(string.Empty, orderCreated);
			}
			catch (Exception exception)
			{
				return BadRequest(exception);
			}
		}

		/// <summary>
		/// List all orders for a specific customer based on given Id.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult GetOrdersByCustomerId(int? customerId)
		{
			try
			{
				if (customerId == null)
				{
					return BadRequest("CustomerId cannot be null.");
				}

				var orders = InventoryManager.GetOrdersByCustomerId(customerId.Value);
				return Ok(orders);
			}
			catch (Exception exception)
			{
				return BadRequest(exception);
			}
		}

		/// <summary>
		/// Cancel order.
		/// </summary>
		/// <returns></returns>
		[HttpDelete]
		public IActionResult CancelOrder(int? orderId)
		{
			try
			{
				if (orderId == null)
				{
					return BadRequest("OrderId cannot be null.");
				}

				var orderUpdated = InventoryManager.CancelOrder(orderId.Value);

				if (orderUpdated == null)
				{
					return NotFound($"Order with Id {orderId} could not be found.");
				}

				return Ok(orderUpdated);
			}
			catch (Exception exception)
			{
				return BadRequest(exception);
			}
		}

		/// <summary>
		/// Update order for order Id from given parameters.
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		public IActionResult UpdateOrder([FromBody] Order order)
		{
			try
			{
				if (order == null)
				{
					return BadRequest("Order cannot be null.");
				}
				if (order.Id == 0)
				{
					return BadRequest("Order has to be greater than 0.");
				}

				var orderUpdated = InventoryManager.UpdateOrder(order);

				if (orderUpdated == null)
				{
					return NotFound("Order could not be found.");
				}

				return Ok(orderUpdated);
			}
			catch (Exception exception)
			{
				return BadRequest(exception);
			}
		}
	}
}