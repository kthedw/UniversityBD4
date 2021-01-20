using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsityAdvertisments.Models
{
    public class Advertisment
    {
        public int id { get; set; }
        public String name { get; set; }
        public String phoneNumber { get; set; }
        public float priceUSD { get; set; }
        public float course { get; set; }
        public float priceBYN { get; set; }

        public Advertisment(int id, String phoneNumber, String name, 
              float priceUSD, float course, float priceBYN)
        {
            this.id = id;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.priceUSD = priceUSD;
            this.course = course;
            this.priceBYN = priceBYN;

        }

        public Advertisment()
        {
            this.id = 0;
            this.name = "";
            this.phoneNumber = "";
            this.priceUSD = 0;
            this.course = 0;
            this.priceBYN = 0;

        }
    }
}
