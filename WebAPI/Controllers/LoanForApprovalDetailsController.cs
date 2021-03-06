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
    public class LoanForApprovalDetailsController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public LoanForApprovalDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.LoanDetails where LoanStatus='For Approval'";
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
            return new JsonResult(table);

        }


        [HttpPut]
        public JsonResult Put(LoanDetails loan)
        {
            string query = @"update dbo.LoanDetails set LoanStatus='"+loan.LoanStatus+@"' 
                where CustomerId= "+loan.CustomerId + @""; 
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
            return new JsonResult("Updated Successfully");

        }




    }
}
