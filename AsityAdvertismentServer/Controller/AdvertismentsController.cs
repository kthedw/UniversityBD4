using AsityAdvertisments.Models;
using AsityAdvertismentServer.Prefs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AsityAdvertismentServer.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdvertismentsController : ControllerBase
    {

        [HttpGet]
        public string GetList()
        {
            Database db = new Database();
            List<Advertisment> advs = new List<Advertisment>();

            SqlConnection conn = new SqlConnection(db.connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [AdvertismentTest].[dbo].[advertisments]", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Advertisment adv = new Advertisment((int)reader["id"], (string)reader["phonenumber"],
                                                        (string)reader["name"], 
                                                        Convert.ToSingle(reader["priceusd"]),
                                                        Convert.ToSingle(reader["course"]), Convert.ToSingle(reader["pricebyn"]));

                    advs.Add(adv);
                }
            }


            conn.Close();

            string output = JsonConvert.SerializeObject(advs, Formatting.None);

            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return output;


        }

        [HttpPost]
        public StatusCodeResult addAdvertisment([FromBody] Advertisment advertisment)
        {
            Database db = new Database();
            SqlConnection conn = new SqlConnection(db.connectionString);
            conn.Open();

            String sqlQuery = $@"INSERT INTO [dbo].[advertisments]
           ([id]
           ,[phonenumber]
           ,[name]
           ,[priceusd]
           ,[course]
           ,[pricebyn])
     VALUES
           ({advertisment.id}
           ,'{advertisment.phoneNumber}'
           ,'{advertisment.name}'
           ,{advertisment.priceUSD.ToString().Replace(",", ".")}
           ,{advertisment.course.ToString().Replace(",", ".")}
           ,{advertisment.priceBYN.ToString().Replace(",", ".")})";
           

            SqlCommand command = new SqlCommand(sqlQuery, conn);
            try
            {
                command.ExecuteNonQuery();
                return StatusCode(200);

            }
            catch
            {
                return StatusCode(500);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}