namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    public class CategoriesManagementEventArgs : EventArgs
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
