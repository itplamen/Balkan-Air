namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    public class FlightStatusesManagementEventArgs : EventArgs
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
