using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UserLogin
    {   
        [Key]
        public int CustomerId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
