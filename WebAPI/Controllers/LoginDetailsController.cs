using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        [HttpPost]
        public JsonResult Post(UserLogin Login_det)
        {
            string query = @"insert into dbo.UserLogin values('" + Login_det.EmailId + @"','" + Login_det.Password + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyConStr");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");

        }

    }
}
