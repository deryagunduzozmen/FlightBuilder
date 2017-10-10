using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuilderProject.DTO;
using FlightBuilderProject.Interfaces;

namespace FlightBuilderProject.BO
{
    public class FlightBuilderOperations : IFlightBuilderOperations
    {
        public FlightBuilderOperations()
        { }

        public List<Flight> GetFilteredFlights(List<Flight> flights)
        {
            List<Flight> filteredFlights = new List<Flight>();
            TimeSpan diff = TimeSpan.Zero;
            int index = 0;
        List<Segment> sg=   flights.SelectMany(x => x.Segments.ToList()).ToList();
            foreach (var flight in flights)
            {
               List<bool> segment2= flight.Segments.Select(x => (x.DepartureDate > x.ArrivalDate)).ToList();
                // linq extension methods(.where) can be used for 1,2 filters it was ok;but for 3rd filter it would be too much complicated and hard to trace and manage it.
                //if (flight.Segments.Where(x => (x.DepartureDate > x.ArrivalDate)).Count()>0 ||
                //    flight.Segments.Where(x=>(x.DepartureDate<DateTime.Now)).Count()>0

                diff = TimeSpan.Zero;
                foreach (var segment in flight.Segments)
                {
                    if (flight.Segments.Last() != segment)
                    {
                        index = flight.Segments.IndexOf(segment);
                        diff += flight.Segments[index + 1].DepartureDate.Subtract(segment.ArrivalDate);
                    }
                    if (segment.DepartureDate > segment.ArrivalDate || segment.DepartureDate < DateTime.Now || diff.Hours > 2)
                    {
                        filteredFlights.Add(flight);
                        break;
                    }
                }
            }
     
            flights.RemoveAll(i => filteredFlights.Contains(i));
            return flights;
        }

    }
}
