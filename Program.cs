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

//Feature 4 - List all boarding gates
void ListBoardingGates()
{
    Console.WriteLine("====================================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("====================================================");
    Console.WriteLine($"{"Gate Name",-10} {"DDJB",-10} {"CFFT",-10} {"LWTT",-10}");
    foreach (BoardingGate boardingGate in terminal.BoardingGates.Values)
    {
        Console.WriteLine($"{boardingGate.GateName,-10} {boardingGate.SupportsDDJB,-10} {boardingGate.SupportsCFFT,-10} {boardingGate.SupportsLWTT,-10}");

    }
}

//Feature 6 - Create a new flight
void CreateFlight()
{
    while (true)

    {
        Console.Write("Enter Flight Number: ");
        string flightNumber = Console.ReadLine();
        Console.Write("Enter Origin: ");
        string origin = Console.ReadLine();
        Console.Write("Enter Destination: ");
        string destination = Console.ReadLine();
        Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm):");
        DateTime expectedTime = Convert.ToDateTime(Console.ReadLine());
        Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None):");
        string specialRequestCode = Console.ReadLine();
        Flight newFlight = null;

        if (specialRequestCode == "CFFT")
        {
            newFlight = new CFFTFlight(flightNumber, origin, destination, expectedTime);
        }
        else if (specialRequestCode == "DDJB")
        {
            newFlight = new DDJBFlight(flightNumber, origin, destination, expectedTime);
        }
        else if (specialRequestCode == "LWTT")
        {
            newFlight = new LWTTFlight(flightNumber, origin, destination, expectedTime);
        }
        else if (specialRequestCode == "None")
        {
            newFlight = new NORMFlight(flightNumber, origin, destination, expectedTime);
        }

        terminal.Flights[flightNumber] = newFlight;
        Console.WriteLine($"Flight {newFlight.FlightNumber} has been added!");
        Console.Write("Would you like to add another flight? (Y/N)");
        string response = Console.ReadLine().ToUpper();

        if (response == "N") { break; }

    }

}

//Feature 7 -- Display flight details from an airline
void DisplayAirlineFlights()
{
    Console.WriteLine("========================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("========================================");
    Console.WriteLine($"{"Airline Code",-15} {"Airline Name",-20}");
    foreach (Airline airline in terminal.Airlines.Values)
    {
        Console.WriteLine($"{airline.Code, -15} {airline.Name, -20}");

    }
    Console.Write("Enter Airline Code: ");
    string airlineCode = Console.ReadLine();

    Airline selectedAirline = terminal.Airlines[airlineCode];

    Console.WriteLine("========================================");
    Console.WriteLine($"List of Flights for {selectedAirline.Name}");
    Console.WriteLine("========================================");

    foreach (Flight flight in terminal.Flights.Values)
    {
        
        Airline airline = terminal.GetAirlineFromFlight(flight);
        if (airline == selectedAirline)
        {
            selectedAirline.AddFlight(flight);
          
        }
    }
    Console.WriteLine($"{"Flight Number", -15} {"Airline Name",-20} {"Origin",-15} {"Destination",-15} {"Expected Departure/Arrival Time",-30} {"Special Request Code",-20}");
    

    foreach (Flight flight in selectedAirline.Flights.Values)
    {
        string specialRequestCode = "NONE";
        if (flight is CFFTFlight)
        {
            specialRequestCode = "CFFT";
        }
        else if (flight is DDJBFlight)
        {
            specialRequestCode = "DDJB";
        }
        else if (flight is LWTTFlight)
        {
            specialRequestCode = "LWTT";
        }

        Console.WriteLine($"{flight.FlightNumber, -15} {selectedAirline.Name, -20} {flight.Origin, -15} {flight.Destination, -15} {flight.ExpectedTime, -30} {specialRequestCode, -20}");
    }
}

//Display Menu
void DisplayMenu()
{
    Console.WriteLine("========================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("========================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
    Console.WriteLine();
    Console.Write("Enter your choice: ");
}


