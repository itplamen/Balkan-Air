namespace BalkanAir.Mvp.Models.Administration
{
    using System.Collections.Generic;

    using Data.Models;
    
    public class AdministrationHomeViewModel
    {
        public IEnumerable<User> Administrators { get; set; }
    }
}
