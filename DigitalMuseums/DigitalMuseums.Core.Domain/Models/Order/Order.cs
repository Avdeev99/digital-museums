using System;
using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Models.Adjacent;
using DigitalMuseums.Core.Domain.Models.Auth;

namespace DigitalMuseums.Core.Domain.Models.Order
{
    public class Order : BaseEntity
    {
        public OrderStatus Status { get; set; }

        public DateTime Created { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<SouvenirOrderDetail> OrderDetails { get; set; }
    }
}