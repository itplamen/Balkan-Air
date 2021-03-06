﻿namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;

    public class AirportsManagementViewModel
    {
        public IQueryable<Airport> Airports { get; set; }

        public IQueryable<Country> Countries { get; set; }
    }
}
