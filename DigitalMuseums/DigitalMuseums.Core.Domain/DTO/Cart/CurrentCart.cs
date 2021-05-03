using System;
using System.Collections.Generic;

namespace DigitalMuseums.Core.Domain.DTO.Cart
{
    public class CurrentCart
    {
        public DateTime Created { get; set; }

        public int UserId { get; set; }
        
        public IEnumerable<CurrentCartDetail> OrderDetails { get; set; }
    }
}