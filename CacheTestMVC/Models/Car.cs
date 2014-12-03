using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CacheTestMVC.Models
{
    public class Car
    {

        public Car()
        {}

        public Car(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            Name = Convert.ToString(record["Name"]);
            Brand = Convert.ToString(record["Brand"]);
            Year = Convert.ToDateTime(record["Year"]);

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public DateTime Year { get; set; }

    }
}