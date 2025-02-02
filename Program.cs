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

//Feature 5 - Assign a boarding gate to a flight
void AssignBoardingGate()
{
    Console.WriteLine("===================================================");
    Console.WriteLine("Assign Boarding Gate to a Flight");
    Console.WriteLine("===================================================");

    string flightNumber;
    Flight flight;
    while (true)
    {
        //validation of the input of flight number 
        Console.Write("Enter Flight Number: ");
        flightNumber = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(flightNumber))
        {
            Console.WriteLine("Flight Number cannot be empty!");
            continue;
        }
        if (!terminal.Flights.ContainsKey(flightNumber))
        {
            Console.WriteLine("Invalid response. Please enter a valid Flight Number!");
        }
        else
        {
            flight = terminal.Flights[flightNumber];
            break;
        }
    }

    //check for special request code
    string specialRequestCode = "None";
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

    BoardingGate currentAssignedGate = null;

    foreach (BoardingGate gateName in terminal.BoardingGates.Values)
    {
        if (gateName.Flight != null && gateName.Flight.FlightNumber == flightNumber)
        {
            currentAssignedGate = gateName;
            break;  // exit  the loop once the gate is found
        }
    }

    if (currentAssignedGate != null)
    {
        Console.WriteLine($"The Flight {flightNumber} is currently assigned to Boarding Gate {currentAssignedGate.GateName}.");
        Console.Write("Do you wish to reassign this flight to a new gate? (Y/N): ");
        while (true)
        {
            string option = Console.ReadLine().ToUpper();
            if (option == "Y")
            {
                currentAssignedGate.Flight = null;  // remove the flight assignment from the current gate
                break;
            }
            else if (option == "N")
            {
                Console.WriteLine("The existing assignment will be kept!");
                return;
            }
            else
            {
                Console.WriteLine("Invalid response. Please enter 'Y' or 'N'!");
            }
        }
    }

   
    BoardingGate gate;

    while (true)
    {
        Console.Write("Enter Boarding Gate: ");
        string gateName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(gateName))
        {
            Console.WriteLine("Gate Name cannot be empty!");
            continue;
        }

        if (!terminal.BoardingGates.ContainsKey(gateName))
        {
            Console.WriteLine("Invalid Boarding Gate Entered. Please try again!");
            continue;
        }

        gate = terminal.BoardingGates[gateName];

        //validate if the special request codes between flights and boarding gates match
        if ((specialRequestCode == "CFFT" && !gate.SupportsCFFT) ||
            (specialRequestCode == "DDJB" && !gate.SupportsDDJB) ||
            (specialRequestCode == "LWTT" && !gate.SupportsLWTT))
        {
            Console.WriteLine($"Boarding Gate {gateName} does not support flights with the '{specialRequestCode}' special request. Please enter a compatible gate!");
            continue;
        }
        if (gate.Flight != null)
        {
            Console.WriteLine($"Boarding Gate {gateName} is already assigned to Flight {gate.Flight.FlightNumber}. Please enter a different Boarding Gate!");
            continue;
        }
        gate.Flight = flight;
        break;
    }
    Console.WriteLine($"Flight Number: {flight.FlightNumber}");
    Console.WriteLine($"Origin: {flight.Origin}");
    Console.WriteLine($"Destination: {flight.Destination}");
    Console.WriteLine($"Expected Time: {flight.ExpectedTime}");
    Console.WriteLine($"Special Request Code: {specialRequestCode}");
    Console.WriteLine($"Boarding Gate Name: {gate.GateName}");
    Console.WriteLine($"Supports DDJB: {gate.SupportsDDJB}");
    Console.WriteLine($"Supports CFFT: {gate.SupportsCFFT}");
    Console.WriteLine($"Supports LWTT: {gate.SupportsLWTT}");

    //set status of flight
    Console.Write("Would you like to update the status of the flight (Y/N): ");
    string response = Console.ReadLine().ToUpper();
    while (true)
    {
        if (response == "Y")
        {
            while (true)
            {
                Console.WriteLine("Select New Status:");
                Console.WriteLine("1. Delayed");
                Console.WriteLine("2. Boarding");
                Console.WriteLine("3. On Time");
                Console.Write("Please select the new status of the flight: ");
                string statusChoice = Console.ReadLine();

                if (statusChoice == "1")
                {
                    flight.Status = "Delayed";
                    break;
                }
                else if (statusChoice == "2")
                {
                    flight.Status = "Boarding";
                    break;
                }
                else if (statusChoice == "3")
                {
                    flight.Status = "On Time";
                    break;
                }
                else if (string.IsNullOrWhiteSpace(statusChoice))
                {
                    Console.WriteLine("Input cannot be empty!");
                    continue;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1, 2, or 3!");
                }
            }
            break;
        }
        else if (response == "N")
        {
            Console.WriteLine("No changes for Flight Status.");
            break;
        }
        else
        {
            Console.WriteLine("Invalid response. Please enter 'Y' or 'N'!");
            Console.Write("Would you like to update the Flight Status? [Y/N]: ");
            response = Console.ReadLine().ToUpper();
        }
    
    }
    Console.WriteLine($"Flight {flight.FlightNumber} has been assigned to Boarding Gate {gate.GateName}!");
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
        Console.WriteLine("6. Display the total Airline Fees");
        Console.WriteLine("0. Exit");
        Console.WriteLine();
        Console.Write("Please select your option: ");
        Console.WriteLine();
    }

while (true)
{
    DisplayMenu();
    string choice = Console.ReadLine();
    if (choice == "1")
    {
        ListAllFlights();
    }
    else if (choice == "2")
    {
        ListBoardingGates();
    }
    else if (choice == "3")
    {
        AssignBoardingGate();
    }
    else if (choice == "4")
    {
        CreateFlight();
    }
    else if (choice == "5")
    {
        DisplayAirlineFlights();
    }
    else if (choice == "6")
    {
        CalculateTotalFees();
    }
    else if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
        break;
    }
    else if (string.IsNullOrWhiteSpace(choice))
    {
        Console.WriteLine("Input cannot be empty!");
        continue;
    }
    else
    {
        Console.WriteLine("Invalid response. Please enter a valid option (1/2/3/4/5/6/0)! ");
    }
}

