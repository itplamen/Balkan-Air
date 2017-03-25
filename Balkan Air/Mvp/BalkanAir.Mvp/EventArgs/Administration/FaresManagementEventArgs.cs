namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    public class FaresManagementEventArgs : EventArgs
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int RouteId { get; set; }
    }
}
