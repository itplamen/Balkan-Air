﻿namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface IFlightStatusesServices
    {
        int AddFlightStatus(FlightStatus flightStatus);

        FlightStatus GetFlightStatus(int id);

        IQueryable<FlightStatus> GetAll();

        FlightStatus UpdateFlightStatus(int id, FlightStatus flightStatus);

        FlightStatus DeleteFlightStatus(int id);
    }
}
