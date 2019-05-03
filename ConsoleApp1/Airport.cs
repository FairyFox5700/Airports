using System;
using System.Collections.Generic;
using System.Text;

namespace Airports
{
    public class Airport
    {
        public string AirportId { get; set; }
        public string AirportName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string IATA { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
