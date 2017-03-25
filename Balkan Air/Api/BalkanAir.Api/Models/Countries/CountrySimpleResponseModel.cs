namespace BalkanAir.Api.Models.Countries
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class CountrySimpleResponseModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}