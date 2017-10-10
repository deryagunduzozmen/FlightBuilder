using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuilderProject.BO;
using FlightBuilderProject.DTO;

//project is runnable in vs 2013 ref: https://msdn.microsoft.com/en-us/library/hh266747.aspx 

namespace FlightBuilderProject
{
    class Program
    {
        static void Main(string[] args)
        {
            FlightBuilder fb = new FlightBuilder();
            List<Flight> flights = fb.GetFlights().ToList();  //to use removeall converted to  List<T>
            Console.WriteLine("Flights and their segments:\n");
            PrintFlights(flights);
            FlightBuilderOperations flightBuilder = new FlightBuilderOperations();
            List<Flight> finalFlights = flightBuilder.GetFilteredFlights((flights));
            //final Flights; remove first filtered flights from firs list of flights  

            Console.WriteLine("\nFilters:");
            Console.WriteLine("1)Depart before the current date/time");
            Console.WriteLine("2)Have a segment with an arrival date before the departure date");
            Console.WriteLine("3)Spend more than 2 hours on the ground\n");
            Console.WriteLine("Final flights and their segments:\n");
            PrintFlights(finalFlights);
            Console.ReadLine();
        }

        private static void PrintFlights(List<Flight> flights)
        {
            foreach (var flight in flights)
            {
                foreach (var segment in flight.Segments)
                {
                    Console.WriteLine("Flight{0} of Segment{1}'s Departuredate:{2},Arrivaldate:{3}, ", flights.IndexOf(flight) + 1, flight.Segments.IndexOf(segment) + 1, segment.DepartureDate, segment.ArrivalDate);
                }
            }
        }
    }
}
