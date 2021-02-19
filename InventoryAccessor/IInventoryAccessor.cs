using System.Collections.Generic;
using DTOs = Accessors.DataTransferObjects;

namespace Accessors
{
    public interface IInventoryAccessor
    {
        DTOs.Order CreateOrder(DTOs.Order order);

        List<DTOs.Order> GetOrdersByCustomerId(int customerId);

        string CancelOrder(int OrderId);

        DTOs.Order UpdateOrder(DTOs.Order order);
    }
}
