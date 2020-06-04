using ParkingAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingAPI.Controllers
{
    [RoutePrefix("api/ParkingRequest")]
    public class ParkingRequestController : ApiController
    {
        //Test comments
        string SQLConnstr = "Server=B2ML27954;Database=ParkingDB;Integrated Security=SSPI;";
        // GET: api/ParkingRequest
        [Route("Requests")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ParkingRequest/5
        [Route("RequestById/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ParkingRequest
        [Route("Request")]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ParkingRequest/5
        [Route("UpdateRequestById")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ParkingRequest/5
        [Route("DeleteRequestById/{id}")]
        public void Delete(int id)
        {
        }

        // GET: api/ParkingRequest
        [Route("Cities")]
        [HttpGet]
        public IEnumerable<City> Cities()
        {
            List<City> lstCity = new List<City>();
            City city;
            using (SqlConnection connection = new SqlConnection(SQLConnstr))
            {
                SqlCommand command = new SqlCommand("Select * from City", connection);
                command.CommandType = System.Data.CommandType.Text;
                SqlDataReader dataReader = null;
                connection.Open();
                using (dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        city = new City
                        {
                            CityId = Convert.ToInt32(dataReader[0]),
                            CityName = dataReader[1].ToString()
                        };
                        lstCity.Add(city);
                    }
                };
            }; 
            return lstCity;
        }

        [Route("Providers")]
        [HttpGet]
        public IEnumerable<Provider> Providers()
        {
            List<Provider> lstProvider = new List<Provider>();
            Provider provider; 
            using (SqlConnection connection = new SqlConnection(SQLConnstr))
            {
                SqlCommand command = new SqlCommand("Select * from Providers", connection);
                command.CommandType = System.Data.CommandType.Text;
                SqlDataReader dataReader = null;
                connection.Open();
                using (dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        provider = new Provider
                        {
                            ProviderId = Convert.ToInt32(dataReader[0]),
                            ProviderName = dataReader[1].ToString()
                        };
                        lstProvider.Add(provider);
                    }
                };
            };            
            return lstProvider;
        }

        [Route("{CityId}/Providers")]
        [HttpGet]
        public IEnumerable<Provider> ProvidersByCity(int CityId)
        {
            List<Provider> lstProvider = new List<Provider>();
            Provider provider;
            using (SqlConnection connection = new SqlConnection(SQLConnstr))
            {
                SqlCommand command = new SqlCommand("Select ProviderId,ProviderName from Providers where City_Id=" + CityId, connection);
                command.CommandType = System.Data.CommandType.Text;
                SqlDataReader dataReader = null;
                connection.Open();
                using (dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        provider = new Provider
                        {
                            ProviderId = Convert.ToInt32(dataReader[0]),
                            ProviderName = dataReader[1].ToString()
                        };
                        lstProvider.Add(provider);
                    }
                };
            };
            return lstProvider;            
        }

        [Route("ProviderById/{ProviderId}")]
        [HttpGet]
        public Provider Provider(int ProviderId)
        {
            return new Provider { ProviderId = 1, ProviderName = "Provider1" };
        }
    }
}
