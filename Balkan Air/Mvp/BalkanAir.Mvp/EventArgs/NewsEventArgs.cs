namespace BalkanAir.Mvp.EventArgs
{
    public class NewsEventArgs
    {
        public NewsEventArgs(int id)
        {
            this.Id = id;
        }

        public int Id { get; private set; }
    }
}
