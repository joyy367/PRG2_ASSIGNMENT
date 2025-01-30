using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10269287_PRG2Assignment
{
    class Flight
{
    public string FlightNumber { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime ExpectedTime { get; set; }
    public string Status { get; set; }
    public Flight() { }

    public Flight(string flightNumber, string origin, string destination, DateTime expectedTime)
    {
        FlightNumber = flightNumber;
        Origin = origin;
        Destination = destination;
        ExpectedTime = expectedTime;
    }
    public virtual double CalculateFees()
    {
        return 800.0;
    }
    public override string ToString()
    {
        return "Flight Number: " + FlightNumber + " Origin: " + Origin + " Destination: " + Destination + " Expected Time: " + ExpectedTime;
    }

}
}
