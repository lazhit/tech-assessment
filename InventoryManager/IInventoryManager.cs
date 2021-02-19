using System.Collections.Generic;
using Contracts = Managers.Contracts;

namespace Managers
{
    public interface IInventoryManager
    {
        Contracts.Order CreateOrder(Contracts.Order order);

        List<Contracts.Order> GetOrdersByCustomerId(int customerId);

        string CancelOrder(int OrderId);

        Contracts.Order UpdateOrder(Contracts.Order order);
    }
}
