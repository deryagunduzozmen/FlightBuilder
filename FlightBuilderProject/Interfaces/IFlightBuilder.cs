using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuilderProject.DTO;

namespace FlightBuilderProject.Interfaces
{
    //Better to create interface of FlightBuilder and inherite it from interface
    public interface IFlightBuilder
    {
        IList<Flight> GetFlights();
        Flight CreateFlight(params DateTime[] dates);
    }
}
