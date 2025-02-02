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
        //add to respective airline dict
        Airline airline = terminal.GetAirlineFromFlight(newFlight);
        airline.AddFlight(newFlight);

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
        string assignedFlight;

        if (boardingGate.Flight != null) // check if any flight is assigned to the gate
        {
            assignedFlight = boardingGate.Flight.FlightNumber;
        }
        else
        {
            assignedFlight = "None";
        }

        // display the boarding gate details
        Console.WriteLine($"{boardingGate.GateName,-10} {boardingGate.SupportsDDJB,-10} {boardingGate.SupportsCFFT,-10} {boardingGate.SupportsLWTT,-10} {assignedFlight,-20}");
    }
}

//Feature 6 - Create a new flight
void CreateFlight()
{
    while (true)

    {
        string flightNumber;
        //validate flight number input
        while (true)
        {
            Console.Write("Enter Flight Number: ");
            flightNumber = Console.ReadLine().ToUpper();
        
            if (string.IsNullOrWhiteSpace(flightNumber))
            {
                Console.WriteLine("Flight number cannot be empty!");
                continue;
            }
        
            string[] flightParts = flightNumber.Split(' ');
        
            if (flightParts.Length != 2)
            {
                Console.WriteLine("Invalid flight number format. Please enter a flight number in the correct format (eg. SQ 123)!!");
                continue;
            }
        
            string airlineCode = flightParts[0].ToUpper(); 
            string numberPart = flightParts[1];      
        
            if (!terminal.Airlines.ContainsKey(airlineCode))
            {
                Console.WriteLine($"Invalid airline code '{airlineCode}'. Please enter a valid 2-letter Airline Code!");
                continue;
            }
        
            if (!numberPart.All(char.IsDigit))
            {
                Console.WriteLine("Flight number part must be numeric. (eg. SQ 123)");
                continue;
            }
        
            if (terminal.Flights.ContainsKey(flightNumber))
            {   
                Console.WriteLine($"Flight {flightNumber} already exists. Please enter a unique flight number.");
                continue;
            }
        
                break; 
            }
        //validate origin input
        string origin;
        
        while (true)
        {
            Console.Write("Enter Origin: ");
            origin = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(origin))
            {
                Console.WriteLine("Origin cannot be empty!");
                continue;
            }
        break;
        }
        //validate destination input
        
        string destination;
        while (true)
        {
            Console.Write("Enter Destination: ");
            destination = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(destination))
            {
                Console.WriteLine("Destination cannot be empty!");
                continue;
            }
            break;
        }
        //validate date time input
        DateTime expectedTime;
        while (true)
        {
            try
            {
                Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                expectedTime = Convert.ToDateTime(Console.ReadLine());
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date. Please enter the date and time in the correct format (dd/mm/yyyy hh:mm).");
            }
        }
        
        //validate special request code input
        string specialRequestCode;
        while (true)
        {
            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            specialRequestCode = Console.ReadLine().ToUpper();
            if (string.IsNullOrWhiteSpace(specialRequestCode))
            {
                Console.WriteLine("Special Request Code cannot be empty!");
                continue;
            }
            if (specialRequestCode != "CFFT" && specialRequestCode != "DDJB" && specialRequestCode != "LWTT" && specialRequestCode != "NONE")
            {
                Console.WriteLine("Invalid special request code. Please enter CFFT, DDJB, LWTT, or None.");
                continue;
            }
            break;
        }
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
        Airline airline = terminal.GetAirlineFromFlight(newFlight);
        airline.AddFlight(newFlight);

        //append new flight details to file
        using (StreamWriter sw = new StreamWriter("flights.csv", true))
        {
            sw.WriteLine($"{newFlight.FlightNumber},{newFlight.Origin},{newFlight.Destination},{newFlight.ExpectedTime:dd/MM/yyyy HH:mm},{specialRequestCode}");
        }

        string response;
        while (true)
        {
            Console.Write("Would you like to add another flight? (Y/N): ");
            response = Console.ReadLine().ToUpper();
            if (string.IsNullOrWhiteSpace(response))
            {
                Console.WriteLine("Input cannot be empty!");
                continue;
            }
            if (response != "Y" && response != "N")
            {
                 Console.WriteLine("Invalid response. Please enter 'Y' or 'N'!");
                continue;
            }
            break;
        }

        if (response == "N") 
        { 
            break; 
        }


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
   string airlineCode;
    //validate airline code input
    while (true)
    {
        Console.Write("Enter Airline Code: ");
        airlineCode = Console.ReadLine().ToUpper();
    
        if (string.IsNullOrWhiteSpace(airlineCode))
        {
            Console.WriteLine("Airline Code cannot be empty.");
            continue;
        }
        if (!terminal.Airlines.ContainsKey(airlineCode))
        {
    
            Console.WriteLine($"Invalid Airline Code '{airlineCode}'. Please enter a valid 2-letter Airline Code!");
            continue;
        }
        break;
    }
    Airline selectedAirline = terminal.Airlines[airlineCode];

    Console.WriteLine("========================================");
    Console.WriteLine($"List of Flights for {selectedAirline.Name}");
    Console.WriteLine("========================================");

    Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-20} {"Origin",-15} {"Destination",-15} {"Expected Departure/Arrival Time",-35} {"Special Request Code",-20} {"Gate Assigned", -10}");


    //check for speical request code
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
        string assignedGate = "None";
        foreach (BoardingGate gate in terminal.BoardingGates.Values)
        {
            if (gate.Flight != null && gate.Flight.FlightNumber == flight.FlightNumber)
            {
                assignedGate = gate.GateName;  
                break;  
            }
        }

        Console.WriteLine($"{flight.FlightNumber,-15} {selectedAirline.Name,-20} {flight.Origin,-15} {flight.Destination,-15} {flight.ExpectedTime,-35} {specialRequestCode,-20} {assignedGate,-10}");
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

//Advanced Feature - Display the total fee per airline for the day
void CalculateTotalFees()
{
    Console.WriteLine("========================================");
    Console.WriteLine("Total Fee Per Airline for the Day");
    Console.WriteLine("========================================");

    List<string> unassignedFlights = new List<string>();

    foreach (Flight flight in terminal.Flights.Values)
    {
        bool isAssigned = false;

        //to check if any gate has the flight assigned
        foreach (BoardingGate gate in terminal.BoardingGates.Values)
        {
            if (gate.Flight != null && gate.Flight.FlightNumber == flight.FlightNumber)
            {
                isAssigned = true;
                break;  // Exit the loop 
            }
        }
        //adding unassigned flights to the list
        if (!isAssigned)
        {
            unassignedFlights.Add(flight.FlightNumber);
        }
    }

    //displaying the unassigned flights 
    if (unassignedFlights.Count > 0)
    {
        Console.WriteLine("The flights below have not been assigned a boarding gate:");
        foreach (string flightNumber in unassignedFlights)
        {
            Console.WriteLine($"Flight {flightNumber}");
        }
        Console.WriteLine("Please assign boarding gates to all flights before running this feature again!");
        return;
    }


    Dictionary<string, double> airlineFees = new Dictionary<string, double>();
        Dictionary<string, double> airlineDiscounts = new Dictionary<string, double>();

        double totalAirlineFees = 0;
        double totalAirlineDiscounts = 0;
        Console.WriteLine("Breakdown of fees for each Airline and their flights");
        Console.WriteLine("=====================================================");
        foreach (Airline airline in terminal.Airlines.Values)
        {
            double totalFees = 0;
            double totalDiscounts = 0;
            int flightCount = airline.Flights.Count;
        
            Console.WriteLine($"{airline.Name} ({airline.Code}) Flights Fees:");

            foreach (Flight flight in airline.Flights.Values)
            {
                double flightFee = flight.CalculateFees();
                //check if origin or destination is Singapore
                if (flight.Origin.Contains("SIN"))
                {
                    flightFee += 800;  
                }
                if (flight.Destination.Contains("SIN"))
                {
                    flightFee += 500;  
                }


                flightFee += 300; //boarding gate base fee

                Console.WriteLine($"Flight {flight.FlightNumber} Fee: ${flightFee}");

                totalFees += flightFee;
            }
            

            //discount

            int setsOfThreeFlights = flightCount / 3;
            totalDiscounts += setsOfThreeFlights * 350;

  
            if (flightCount > 5)
            {
                totalDiscounts += totalFees * 0.03;
            }

    
            foreach (Flight flight in airline.Flights.Values)
            {
                if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour >= 21)
                {
                    totalDiscounts += 110;
                }

                if (flight.Origin == "Dubai (DXB)" || flight.Origin == "Bangkok (BKK)" || flight.Origin == "Tokyo (NRT)")
                {
                    totalDiscounts += 25;
                }

                if (flight is NORMFlight) 
                {
                    totalDiscounts += 50;
                }
            }

            airlineFees[airline.Code] = totalFees; //before discount
            airlineDiscounts[airline.Code] = totalDiscounts;
            double finalFee = totalFees - totalDiscounts;

            totalAirlineFees += totalFees;
            totalAirlineDiscounts += totalDiscounts;

            Console.WriteLine($"Breakdown of fees for {airline.Name} ({airline.Code}):");
            Console.WriteLine($"Total Fees: ${totalFees:F2}");
            Console.WriteLine($"Total Discounts: ${totalDiscounts:F2}");
            Console.WriteLine($"Final Total: ${finalFee:F2}\n");
        }

        //summary of total fees for all airlines in terminal 5
        double finalTotalFees = totalAirlineFees - totalAirlineDiscounts;
        double discountPercentage = (totalAirlineDiscounts / totalAirlineFees) * 100;
        
        Console.WriteLine("========================================================");
        Console.WriteLine("Breakdown of Airline fees for Terminal 5:");
        Console.WriteLine($"Total Fees Collected from All the Airlines: ${totalAirlineFees:F2}");
        Console.WriteLine($"Total Discounts Applied: ${totalAirlineDiscounts:F2}");
        Console.WriteLine($"Final Fees Terminal 5 Will Collect: ${finalTotalFees:F2}");
        Console.WriteLine($"Percentage of Discounts: {discountPercentage:F2}%");
        Console.WriteLine("========================================================");
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

