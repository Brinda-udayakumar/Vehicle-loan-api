using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class EmploymentDetails
    {

        public int CustomerId { get; set; }
        public string TypeOfEmployment { get; set; }
        public int AnnualSalary { get; set; }
        public string ExistingEMI { get; set; }
        
    }
}
