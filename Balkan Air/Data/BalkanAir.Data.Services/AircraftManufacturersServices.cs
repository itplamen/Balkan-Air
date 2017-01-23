namespace BalkanAir.Data.Services
{
    using System.Linq;

    using BalkanAir.Data.Services.Contracts;
    using Models;
    using Repositories.Contracts;

    public class AircraftManufacturersServices : IAircraftManufacturersServices
    {
        private readonly IRepository<AircraftManufacturer> manufacturers;

        public AircraftManufacturersServices(IRepository<AircraftManufacturer> manufacturers)
        {
            this.manufacturers = manufacturers;
        }

        public int AddManufacturer(AircraftManufacturer manufacturer)
        {
            this.manufacturers.Add(manufacturer);
            this.manufacturers.SaveChanges();

            return manufacturer.Id;
        }

        public AircraftManufacturer GetManufacturer(int id)
        {
            return this.manufacturers.GetById(id);
        }

        public IQueryable<AircraftManufacturer> GetAll()
        {
            return this.manufacturers.All();
        }

        public AircraftManufacturer UpdateManufacturer(int id, AircraftManufacturer manufacturer)
        {
            var manufacturerToUpdate = this.manufacturers.GetById(id);

            if (manufacturerToUpdate != null)
            {
                manufacturerToUpdate.Name = manufacturer.Name;
                manufacturerToUpdate.IsDeleted = manufacturer.IsDeleted;
                this.manufacturers.SaveChanges();
            }

            return manufacturerToUpdate;
        }

        public AircraftManufacturer DeleteManufacturer(int id)
        {
            var manufacturerToDelete = this.manufacturers.GetById(id);

            if (manufacturerToDelete != null)
            {
                manufacturerToDelete.IsDeleted = true;
                this.manufacturers.SaveChanges();
            }

            return manufacturerToDelete;
        }
    }
}
