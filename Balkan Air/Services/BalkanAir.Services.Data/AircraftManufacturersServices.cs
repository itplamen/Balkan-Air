namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Common;
    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Contracts;

    public class AircraftManufacturersServices : IAircraftManufacturersServices
    {
        private readonly IRepository<AircraftManufacturer> manufacturers;

        public AircraftManufacturersServices(IRepository<AircraftManufacturer> manufacturers)
        {
            this.manufacturers = manufacturers;
        }

        public int AddManufacturer(AircraftManufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.manufacturers.Add(manufacturer);
            this.manufacturers.SaveChanges();

            return manufacturer.Id;
        }

        public AircraftManufacturer GetManufacturer(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.manufacturers.GetById(id);
        }

        public IQueryable<AircraftManufacturer> GetAll()
        {
            return this.manufacturers.All();
        }

        public AircraftManufacturer UpdateManufacturer(int id, AircraftManufacturer manufacturer)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (manufacturer == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

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
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

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
