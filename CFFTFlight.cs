﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10269287_PRG2Assignment
{
    class CFFTFlight : Flight
{
    public CFFTFlight() { }
    public CFFTFlight(string flightName, string origin, string destination, DateTime expectedTime) : base(flightName, origin, destination, expectedTime) { }
    public override double CalculateFees()
    {
        double fee = base.CalculateFees() + 150;
        return fee;
    }
    public override string ToString()
    {
        return base.ToString() + "Flight Type: CFFT";
    }
}
}
