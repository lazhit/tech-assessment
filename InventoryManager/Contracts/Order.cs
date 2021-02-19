using System;

namespace Managers.Contracts
{
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateLastModified { get; set; }

        public int CustomerId { get; set; }
    }
}