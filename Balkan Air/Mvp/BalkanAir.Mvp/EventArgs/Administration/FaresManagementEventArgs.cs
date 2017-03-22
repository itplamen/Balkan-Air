namespace BalkanAir.Mvp.EventArgs.Administration
{
    public class FaresManagementEventArgs
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int RouteId { get; set; }
    }
}
