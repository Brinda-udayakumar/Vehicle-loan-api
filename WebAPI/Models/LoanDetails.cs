using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class LoanDetails
    {
        public int CustomerId { get; set; }

        public int LoanAmount { get; set; }
        public int LoanTenure { get; set; }
        public int RateOfInterest { get; set; }
        public string LoanStatus { get; set; }

    }
        
}
