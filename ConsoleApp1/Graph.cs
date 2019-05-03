using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Linq;

namespace Airports
{
    public class NextAirport
    {
        public string Current { get; set; }
        public string Name  { get; set; }
        public double Weight { get; set; }
        public LinkedList<Airport> adjacementList = new LinkedList<Airport>();
        public NextAirport(string cur, string name, double price, LinkedList<Airport> nextPorts)
        {
            Current = cur;
            Name = name;
            Weight = price;
            adjacementList = nextPorts;
        }

    }
    public class Graph
    {
        private const int PRICE_FOR_1_KILOMETR = 32;



        ///protected internal static Airport Airport { get; set; }
       // protected internal static LinkedList<Airport> adjacementList = new LinkedList<Airport>();

      
        ///https://stackoverflow.com/questions/6366408/calculating-distance-between-two-latitude-and-longitude-geocoordinates/44703178#44703178
        protected internal static double GetPath(Airport source, Airport destination)
        {
            var first = Math.PI * source.Latitude / 180;
            var firts2 = Math.PI * destination.Latitude / 180;
            var second = Math.PI * source.Longitude / 180;
            var second2 = Math.PI * destination.Latitude / 180;
            var theta = source.Longitude - destination.Longitude;
            var rTheta = Math.PI * theta / 180;
            var distance = Math.Sin(first) * Math.Sin(firts2) + Math.Cos(first) * Math.Cos(firts2) * Math.Cos(rTheta);
            distance = Math.Acos(distance);
            distance = distance * 180 / Math.PI;
            distance = distance * 60 * 1.1515 * 1.609344;//in kilometers
            return distance;
        }
        protected internal static double GetPriceByPath(Airport source, Airport destination)
        {
            var distance = GetPath(source, destination);
            return distance * PRICE_FOR_1_KILOMETR;

        }
        protected static internal NextAirport DijkstraMinPath(string sourceCode, string destinationCode)
        {
            try {
                //LinkedList<NextAirport> flight = new LinkedList<NextAirport>();
                var routes = AirlineData.GetRoutes();
                Queue<NextAirport> flight = new Queue<NextAirport>();
                List<Airport> visited = new List<Airport>();
                Stack<NextAirport> result = new Stack<NextAirport>();
                Airport source = AirlineData.GetAirPort(sourceCode);
                Airport destination = AirlineData.GetAirPort(destinationCode);
                NextAirport next = new NextAirport(source, 0, AirlineData.GetNeighbours(sourceCode, routes));//min weight
                var next = AirlineData(so                                                                                             //var ne = next.adjacementList.Contains()
                flight.Enqueue(next);
                result.Push(next);
                //SortQueue(ref flight);
                double BestPrice = 0;
                var counter = 0; 
                //source is visited
                while (flight.Count != 0)
                {
                    //Console.ReadKey();

                    var _flight = flight.Dequeue();//remove first minimum
                    //Console.Write(counter);
                    //Console.WriteLine(_flight.current.IATA);
                    
                    if (visited.Contains(_flight.current))
                        continue;



                    if (_flight.current.IATA == destinationCode)//we are here
                        return _flight;

                    foreach (var neighbour in _flight.adjacementList)
                    {
                        if (neighbour != null)
                        {            
                            if (!visited.Contains(neighbour))
                            {
                                NextAirport _next = new NextAirport(neighbour, BestPrice, AirlineData.GetNeighbours(neighbour.IATA, routes));
                                Console.Write(counter);
                                Console.WriteLine(neighbour.IATA);

                                if (BestPrice == 0)
                                {
                                    BestPrice = GetPriceByPath(_flight.current, neighbour);//_next.weight;
                                }
                                else
                                {
                                    if (BestPrice > _next.weight + _flight.weight)
                                    {
                                        BestPrice = _next.weight + _flight.weight;

                                    }
                                }
                                flight.Enqueue(_next);
                                SortQueue(ref flight);
                                NextAirport path = new NextAirport(_flight.current, BestPrice, AirlineData.GetNeighbours(neighbour.AirportName, routes));
                                result.Push(path);
                                //Console.ReadKey();



                                // if ()

                                //flight.Add(_next);


                            }
                        }


                    }
                    counter++;
                    visited.Add(_flight.current);
                }
                
                Console.ReadKey();

            }
            catch(Exception r)
            {
                Console.WriteLine(r);
            }
            
            return null;
        }

        /*protected static internal void GetNextPorts()
        {
            var route = AirlineData.GetRoutes();
            foreach (var item in route)
            {
                Airport airportSource = AirlineData.GetAirPort(item.SourceName);
                Airport airportDestination = AirlineData.GetAirPort(item.DestinationName);

                if (adjacementList.Count == 0)
                {
                    adjacementList?.AddFirst(airportSource);
                }
                else
                {
                    adjacementList.AddAfter(adjacementList.Last, airportDestination);
                }
                //graph is oriented

            }
        }*/
        protected static internal void SortQueue( ref Queue<NextAirport> queue)
        {

            queue.OrderBy(d => d.weight);
            //return q;
        }
    }
}


/* foreach (var neighbour in next.adjacementList)
 {

     if (neighbour != null)
     {
         NextAirport _next = new NextAirport(neighbour, GetPriceByPath(source, neighbour), AirlineData.GetNeighbours(neighbour.AirportName, routes));
         flight.Enqueue(_next);
         if (neighbour.IATA == "MAN")
         {
             Console.WriteLine("Fi");
             Console.ReadKey();
         }
     }
 }
 visited.Add(source);
 while (flight.Count != 0)
 {
     var last = flight.Count-1 ;
     var _flight = flight[last];
     if (_flight.current.IATA == "LAN")
     {
         Console.WriteLine("Finded");
         Console.ReadKey();
     }
     flight.RemoveAt(last);
     if (visited.Contains(_flight.current))
         continue;
     if (_flight.current.IATA == destinationCode)
         return _flight;

     foreach (var neighbour in _flight.adjacementList)
         {
             if (neighbour != null)
             {
                 if (neighbour.IATA == "LAN")
                 {
                     Console.WriteLine("Fieeeended");
                     Console.ReadKey();
                 }
                 if (!visited.Contains(neighbour))
                 {
                     NextAirport _next = new NextAirport(_flight.current, _flight.weight + GetPriceByPath(_flight.current, neighbour), AirlineData.GetNeighbours(neighbour.AirportName, routes));
                     flight.Add(_next);

                 }
             }
         }
         visited.Add(_flight.current);
         /*  var _flight = flight.Last.Value;
           flight.RemoveLast();
           if (visited.Contains(_flight.current))
               continue;
           if (Equals(_flight.current, destination))
               return _flight;
           else
           {
               foreach (var neighbour in _flight.adjacementList)
               {
                   if (!visited.Contains(neighbour))
                   {
                       NextAirport _next = new NextAirport(_flight.current, _flight.weight+ GetPriceByPath(_flight.current, neighbour), AirlineData.GetNeighbours(neighbour.AirportName));
                       if (flight.Count == 0)
                       {
                           flight?.AddFirst(_next);
                       }
                       else
                       {
                           flight.AddAfter(flight.Last, _next);
                       }
                   }
               }
               visited.Add(_flight.current);
           }
           */
