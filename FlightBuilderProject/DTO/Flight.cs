using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBuilderProject.DTO
{
    //Better to seperate builder and other classes
    public class Flight 
    {
        public IList<Segment> Segments { get; set; }
    }
}
