﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10269287_PRG2Assignment
{
    class LWTTFlight : Flight
{
    public double RequestFee { get; set; }
    public LWTTFlight() { }

    public LWTTFlight(string flightName, string origin, string destination, DateTime expectedTime) : base(flightName, origin, destination, expectedTime)
    {
        RequestFee = 500;
    }
    public override double CalculateFees()
    {
        double fee = base.CalculateFees() + RequestFee;
        return fee;
    }
    public override string ToString()
    {
        return base.ToString() + "Flight Type: LWTT";
    }
}
}
