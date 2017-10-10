using System;
using System.Collections.Generic;
using FlightBuilderProject.BO;
using FlightBuilderProject.DTO;
using FlightBuilderProject.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FlightBuilderOperationsUnitTest
{
    /// <summary>
    /// Testin of following case:
    /// Invoke the GetFlights method and given the flights returned from the FlightBuilder class filter out those that;
    ///1. Depart before the current date/time.
    ///2. Have a segment with an arrival date before the departure date.
    ///3. Spend more than 2 hours on the ground
    /// </summary>
    /// 
    //To add test data, CreateFlight() is made public

    [TestClass]
    public class FlightBuilderTest
    {
        private IFlightBuilderOperations flightBuilderOperations;
        private IFlightBuilder flightBuilder;
        DateTime _twoDaysFromNow;

        [TestInitialize]
        public void Init()
        {
            flightBuilderOperations = new FlightBuilderOperations();
            flightBuilder = new FlightBuilder();
            _twoDaysFromNow = DateTime.Now.AddDays(2);
        }

        //1. Depart before the current date/time.
        [TestMethod]
        public void TestDepartBeforeCurrentTime()
        {
            //act
            List<Flight> finalFlights = flightBuilderOperations.GetFilteredFlights(flightBuilder.GetFlights().ToList());
            //Assert
            foreach (var flight in finalFlights)
            {
                Assert.IsTrue(flight.Segments.Where(x => (x.DepartureDate > x.ArrivalDate)).Count() == 0);
            }
        }

        [TestMethod]
        public void TestDepartBeforeCurrentTimeV2()
        {
            //acc
            List<Flight> flightList = new List<Flight> { flightBuilder.CreateFlight(_twoDaysFromNow.AddDays(-4), _twoDaysFromNow) };
            //ass
            Assert.IsTrue(flightBuilderOperations.GetFilteredFlights(flightList).Count == 0);
        }

        //2. Have a segment with an arrival date before the departure date
        [TestMethod]
        public void TestArrivalBeforeDeparture()
        {
            //act
            List<Flight> finalFlights = flightBuilderOperations.GetFilteredFlights(flightBuilder.GetFlights().ToList());
            //Assert
            foreach (var flight in finalFlights)
            {
                Assert.IsTrue(flight.Segments.Where(x => (x.DepartureDate < DateTime.Now)).Count() == 0);
            }

        }

        [TestMethod]
        public void TestArrivalBeforeDepartureV2()
        {
            //acc
            List<Flight> flightList = new List<Flight> { flightBuilder.CreateFlight(_twoDaysFromNow, _twoDaysFromNow.AddHours(-4)) };
            //ass
            Assert.IsTrue(flightBuilderOperations.GetFilteredFlights(flightList).Count == 0);
        }

        //3. Spend more than 2 hours on the ground
        [TestMethod]
        public void TestTwoPlusHourOnGround()
        {
            //assemble
            TimeSpan diff = TimeSpan.Zero;
            int index = 0;
            //act
            List<Flight> finalFlights = flightBuilderOperations.GetFilteredFlights(flightBuilder.GetFlights().ToList());
            //Assert
            foreach (var flight in finalFlights)
            {
                diff = TimeSpan.Zero;
                foreach (var segment in flight.Segments)
                {
                    if (flight.Segments.Last() != segment)
                    {
                        index = flight.Segments.IndexOf(segment);
                        diff += flight.Segments[index + 1].DepartureDate.Subtract(segment.ArrivalDate);
                    }
                    Assert.IsTrue(diff.Hours < 2);
                }
            }
        }

        [TestMethod]
        public void TestTwoPlusHourOnGroundV2()
        {
            //acc
            List<Flight> flightList = new List<Flight> { flightBuilder.CreateFlight(_twoDaysFromNow, _twoDaysFromNow.AddHours(2), _twoDaysFromNow.AddHours(5), _twoDaysFromNow.AddHours(6)), };
            //ass
            Assert.IsTrue(flightBuilderOperations.GetFilteredFlights(flightList).Count == 0);
        }

        //normal flight test
        [TestMethod]
        public void TestNormalFlight()
        {
            //assemble
            int expectedResult = 1;
            //acc
            List<Flight> flightList = new List<Flight> { flightBuilder.CreateFlight(_twoDaysFromNow, _twoDaysFromNow.AddHours(2)) };
            //ass
            Assert.AreEqual(flightBuilderOperations.GetFilteredFlights(flightList).Count, expectedResult);
        }
    }
}
