using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class VehicleDetails
    {

        public int CustomerId { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public float ExShowroomPrice { get; set; }
    }
}
