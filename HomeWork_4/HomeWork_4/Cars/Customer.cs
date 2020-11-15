using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_4
{
    class Customer
    {
        [JsonProperty("car_model")]
        public string CarModel { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}
