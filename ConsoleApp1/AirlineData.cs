using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Airports
{



    public class AirlineData
    {
        // protected internal static List<Airport> listOfAirports = new List<Airport>();
        protected internal static List<Route> listOfRoutes = new List<Route>();
        protected internal static List<Route> listOfNeighbourRoutes = new List<Route>();
        //protected internal static LinkedList<Airport> neighbours = new LinkedList<Airport>();
        protected internal static Airport airport = new Airport();
        protected internal static List<string> l = new List<string>();
        protected static internal Airport GetAirPort(string airportCodeName)
        {
            try
            {
                using (var textFile = System.IO.File.OpenText("airports.txt"))
                {

                    string line = null;

                    while ((line = textFile.ReadLine()) != null)
                    {
                        string[] temp = line.Replace("\"", "").Split(",", System.StringSplitOptions.RemoveEmptyEntries);
                        var name = temp[4].Replace("\"", "");
                        if (temp[4] == airportCodeName)
                        {
                            airport = new Airport
                            {
                                AirportId = temp[0],
                                AirportName = temp[1],
                                CityName = temp[2],
                                CountryName = temp[3],
                                Latitude = double.Parse(temp[6], System.Globalization.CultureInfo.InvariantCulture),
                                IATA = temp[4],
                                Longitude = double.Parse(temp[7], System.Globalization.CultureInfo.InvariantCulture)
                            };
                            return airport;
                        }
                    }
                }
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;
        }

        protected static internal List<Airport> GetAirPort()
        {
            //  try
            //  {
            List<Airport> listOfAirports = new List<Airport>();

            using (var textFile = System.IO.File.OpenText("airports.txt"))
            {

                string line = null;

                while ((line = textFile.ReadLine()) != null)
                {
                    string[] temp = line.Replace("\"", "").Split(",", System.StringSplitOptions.RemoveEmptyEntries);


                    airport = new Airport
                    {
                        AirportId = temp[0].ToString(),
                        AirportName = temp[1].ToString(),
                        CityName = temp[2].ToString(),
                        CountryName = temp[3].ToString(),
                        IATA = temp[4].ToString(),

                    };
                    switch (temp.Length)
                    {
                        case 14:
                            airport.Latitude = double.Parse(temp[6].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                            airport.Longitude = double.Parse(temp[7].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case 15:
                            airport = new Airport
                            {
                                AirportId = temp[0].ToString(),
                                AirportName = temp[1].ToString(),
                                CityName = temp[2].ToString(),
                                CountryName = temp[4].ToString(),
                                IATA = temp[5].ToString(),
                                Latitude = double.Parse(temp[7].ToString(), System.Globalization.CultureInfo.InvariantCulture),
                                Longitude = double.Parse(temp[8].ToString(), System.Globalization.CultureInfo.InvariantCulture)

                            };
                            break;
                        case 13:
                            airport.Latitude = double.Parse(temp[5].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                            airport.Longitude = double.Parse(temp[6].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                            break;
                    }

                    listOfAirports.Add(airport);


                }

            }
            return listOfAirports;
        }
        // catch (Exception a)
        //  {
        //      Console.WriteLine(a.Message);
        //  }
        // return null;




        protected static internal List<Route> GetRoutes()
        {
            try
            {
                using (var textFile = System.IO.File.OpenText("routes.txt"))
                {
                    string line = null;
                    while ((line = textFile.ReadLine()) != null)
                    {
                        string[] temp = line.Split(",");

                        Route route = new Route
                        {
                            SourceId = temp[1],
                            SourceName = temp[2],
                            DestinationId = temp[3],
                            DestinationName = temp[4],
                            NumbefOfStops = temp[5]
                        };
                        listOfRoutes.Add(route);
                    }

                }
                return listOfRoutes;
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;
        }
        protected static internal List<Route> GetRoutes(string codeName)
        {

            try
            {

                foreach (var port in listOfRoutes)
                {
                    if (port.SourceName == codeName)
                        listOfNeighbourRoutes.Add(port);
                }
                return listOfNeighbourRoutes;
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;
        }
        protected static internal LinkedList<Airport> GetNeighbours(string codeName, List<Route> routes)
        {
            try
            {
                LinkedList<Airport> neighbours = new LinkedList<Airport>();
                // listOfRoutes = GetRoutes();
                foreach (var port in routes)
                {
                    if (port.SourceName == codeName)
                    {
                        var next = GetAirPort(port.DestinationName);



                        if (neighbours.Count == 0)
                        {
                            neighbours?.AddFirst(next);
                        }
                        else
                        {
                            neighbours.AddAfter(neighbours.Last, next);
                        }

                    }
                }


                return neighbours;
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;
        }

        protected internal static void WriteCsw()
        {
            try
            {
                var csv = new StringBuilder();
                var routes = GetRoutes();
                var airports = GetAirPort();
                foreach (var airport in airports)
                {
                    if (airport != null)
                    {
                        var source = airport.IATA.ToString();
                        var sourceName = airport.AirportName.ToString();
                        var neighbour = GetNeighbours(source, routes);
                        var adjacements = JsonConvert.SerializeObject(neighbour);
                        var newLine = string.Format("{0},{1},{2}", source, sourceName, adjacements);
                        if (neighbour.Count != 0)
                            csv.AppendLine(newLine);
                    }
                }
                File.WriteAllText("nextStations.txt", csv.ToString());
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }
        protected internal static NextAirport GetNextStation(string stationCode)
        {
            try
            {
                using (var textFile = System.IO.File.OpenText("nextStations.txt"))
                {
                    string line = null;
                    while ((line = textFile.ReadLine()) != null)
                    {
                        string[] temp = line.Split(",", System.StringSplitOptions.RemoveEmptyEntries);
                        if (temp[0] == stationCode)
                        {
                            NextAirport nextAirport = new NextAirport(temp[0], temp[1], -1, JsonConvert.DeserializeObject<LinkedList<Airport>>(temp[2]));
                            return nextAirport;
                        }
                    }
                }
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;

        }
    }
}
    
