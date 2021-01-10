using System;
using System.Collections.Generic;
using DigitalMuseums.Core.Domain.Models.Auth;
using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Domain.Models.Order
{
    public class Order
    {
        public int OrderId { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime Created { get; set; }


        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Souvenir> Souvenirs { get; set; }
    }
}