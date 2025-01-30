using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10269287_PRG2Assignment
{
   class Airline
{
    public string Name { get; set; }
    public string Code { get; set; }

    public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

    public Airline() { }
    public Airline(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public bool AddFlight(Flight flight)
    {
        if (Flights.ContainsKey(flight.FlightNumber))
        {

            return true;
        }
        Flights.Add(flight.FlightNumber, flight);
        return false;
    }

    public double CalculateFees()
    {
        double fees = 0;
        foreach (Flight flight in Flights.Values)
        {
            fees += flight.CalculateFees();

        }
        return fees;
    }

    public bool RemoveFlight(Flight flight)
    {
        if (Flights.ContainsKey(flight.FlightNumber))
        {
            Flights.Remove(flight.FlightNumber);
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return "Name: " + Name + " Code: " + Code;
    }
}
}
