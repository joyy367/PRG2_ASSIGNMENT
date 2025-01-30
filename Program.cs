using S10269287_PRG2Assignment;

//Feature 1 - Loaad Files  (airlines and boarding gates)
Terminal terminal = new Terminal("Terminal 5");
using (StreamReader sr = new StreamReader("airlines.csv"))
{
    string? s = sr.ReadLine();
    while ((s = sr.ReadLine()) != null)

    {
        string[] airlines = s.Split(',');
        string airlineName = airlines[0];
        string airlineCode = airlines[1];
        Airline newAirline = new Airline(airlineName, airlineCode);
        terminal.AddAirline(newAirline);


    }
}

//Feature 2 - Load files (flights)
using (StreamReader sr = new StreamReader("flights.csv"))
{
    string? s = sr.ReadLine();
    while ((s = sr.ReadLine()) != null)

    {
        string[] flights = s.Split(',');
        string flightNumber = flights[0];
        string origin = flights[1];
        string destination = flights[2];
        DateTime expectedTime = Convert.ToDateTime(flights[3]);
        string specialRequestCode = flights[4];
        Flight newFlight;
        if (specialRequestCode == "DDJB")
        {
            newFlight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
        }
        else if (specialRequestCode == "CFFT")
        {
            newFlight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
        }
        else if (specialRequestCode == "LWTT")
        {
            newFlight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
        }
        else
        {
            newFlight = new NORMFlight(flightNumber, origin, destination, expectedTime);
        }
        terminal.Flights[flightNumber] = newFlight;

    }
}

//Feature 3 - List all flights
void ListAllFlights()
{
    Console.WriteLine("========================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("========================================");
    Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-20} {"Origin",-15} {"Destination",-15} {"Expected Departure/Arrival Time",-30}");
    foreach (Flight flight in terminal.Flights.Values)
    {
        Airline airline = terminal.GetAirlineFromFlight(flight);
        string airlineName = airline.Name;
        Console.WriteLine($"{flight.FlightNumber,-15} {airlineName,-20} {flight.Origin,-15} {flight.Destination,-15} {flight.ExpectedTime,-30}");
        
    }
}
