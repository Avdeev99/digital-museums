using System;
using System.Collections.Generic;

namespace DigitalMuseums.Api.Contracts.Responses.Cart
{
    public class GetCurrentCartResponse
    {
        public DateTime Created { get; set; }

        public int UserId { get; set; }
        
        public IEnumerable<CurrentCartDetail> OrderDetails { get; set; }
    }
}