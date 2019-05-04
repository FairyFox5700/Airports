using System;
using Airports;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Graph.GetNextPorts();
            var day =  Graph.DijkstraMinPath("VLC", "PDX");
            //AirlineData.WriteCsw();
           // AirlineData.GetAirPort("GKA");
           // string var = "\"GKA\"";
            //string ne = var.Replace("\\","");
        }
    }
}
