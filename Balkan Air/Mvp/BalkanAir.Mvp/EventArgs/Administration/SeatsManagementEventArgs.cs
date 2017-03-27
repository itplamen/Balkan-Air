namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    public class SeatsManagementEventArgs : EventArgs
    {
        public int Id { get; set; }

        public int Row { get; set; }

        public string Number { get; set; }

        public bool IsReserved { get; set; }

        public int TravelClassId { get; set; }

        public int LegInstanceId { get; set; }
    }
}
