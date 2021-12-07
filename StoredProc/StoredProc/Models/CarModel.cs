using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProcedure.Models
{
    public class Car
    {
        public Guid id { get; set; }
        public string carManufacturer { get; set; }
        public string carModel { get; set; }
        public int carModelYear { get; set; }
        public string carColor { get; set; }
    }
}