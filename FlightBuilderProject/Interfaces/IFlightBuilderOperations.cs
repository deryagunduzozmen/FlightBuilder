using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuilderProject.DTO;

namespace FlightBuilderProject.Interfaces
{
    public interface IFlightBuilderOperations
    {
        List<Flight> GetFilteredFlights(List<Flight> flights);
    }
}
