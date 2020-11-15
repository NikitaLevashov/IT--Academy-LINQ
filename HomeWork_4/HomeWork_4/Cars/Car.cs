using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_4
{
    class Car
    {
        [JsonProperty("car_make")]
        public string CarMake { get; set; }

        [JsonProperty("car_model")]
        public string CarModel { get; set; }

        [JsonProperty("car_year")]
        public int CarYear { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

    }
}
