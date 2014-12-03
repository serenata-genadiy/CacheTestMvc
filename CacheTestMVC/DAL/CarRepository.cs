using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CacheTestMVC.Models;
using System.Configuration;
using Cache;

namespace CacheTestMVC
{
    public class CarRepository
    {
        SqlConnection conn = null;
        string connStr = ConfigurationManager.ConnectionStrings["Main"].ToString();
        public CarRepository()
        {
            conn = new SqlConnection(connStr);
        }

        public IEnumerable<Car> GetCars()
        {
            const string key = "AllCars";


            List<Car> result = new List<Car>();

            // Check if we have cached item for that key. If so obtain the item from the cache.
            if (CacheHelper.IsCached(key))
                return CacheHelper.Get<IEnumerable<Car>>(key);
            

            // We don't have that item in cache so we need to obtain it from DB  

            string sql = "SELECT * from Car";

            using (SqlCommand command = new SqlCommand(sql,conn))
            {
                command.Connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new Car(reader));
                }
            }

            //Store the item in the cache.
            CacheHelper.Add<List<Car>>(result, key);

            return result;


        }

        public bool InsertCar(Car car)
        {

            string sql ="INSERT INTO CAR (Name, Brand, Year) VALUES (@Name, @Brand, @Year)";
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.Connection.Open();

                command.Parameters.Add("@Name", SqlDbType.VarChar);
                command.Parameters["@Name"].Value = car.Name;

                command.Parameters.Add("@Brand", SqlDbType.VarChar);
                command.Parameters["@Brand"].Value = car.Brand;

                command.Parameters.Add("@Year", SqlDbType.Date);
                command.Parameters["@Year"].Value = car.Year;

                Int32 rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

    }
}