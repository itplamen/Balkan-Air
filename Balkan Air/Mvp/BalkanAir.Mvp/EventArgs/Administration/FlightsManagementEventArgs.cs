namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    public class FlightsManagementEventArgs : EventArgs
    {
        public int Id { get; set; }

        public string Number { get; set; }
    }
}
