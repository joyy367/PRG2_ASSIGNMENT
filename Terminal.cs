using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10269287_PRG2Assignment
{
        class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<String, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<String, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        public Dictionary<String, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();

        public Dictionary<String, double> GateFees { get; set; } = new Dictionary<string, double>();

        public Terminal() { }

        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
        }

        public bool AddAirline(Airline airline)
        {
            if (Airlines.ContainsKey(airline.Code))
            {
                return false;
            }
            Airlines.Add(airline.Code, airline);
            return true;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (BoardingGates.ContainsKey(boardingGate.GateName))
            {
                return false;
            }
            BoardingGates.Add(boardingGate.GateName, boardingGate);
            return true;
        }
        public Airline GetAirlineFromFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                string code = flight.FlightNumber.Substring(0, 2);
                if (Airlines.ContainsKey(code))
                {
                    Airline airline = Airlines[code];
                    return airline;
                }
                return null;
            }
            return null;
        }
        public void PrintAirlineFees()
        {
            foreach (Airline airline in Airlines.Values)
            {
                {
                    Console.WriteLine("Airline: " + airline.Name + " Airline Fees: " + airline.CalculateFees());
                }
            }
        }

        public override string ToString()
        {
            return "Terminal Name: " + TerminalName;
        }

    }
}
