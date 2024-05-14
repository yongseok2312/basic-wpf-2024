using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniproject2.Models
{
    public class Mapsource

    {
        public class GeocodingRequest
        {
            public string Address { get; set; }
        }

        public class GeocodingResponse
        {
            public GeocodingResult[] Results { get; set; }
        }

        public class GeocodingResult
        {
            public Geometry Geometry { get; set; }
        }

        public class Geometry
        {
            public Location Location { get; set; }
        }

        public class Location
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}
