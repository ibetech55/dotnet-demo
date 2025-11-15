namespace BrandMicroservice.src.Models.DbModels
{
    public class Make
    {
        public Guid Id { get; set; }

        public required string MakeName { get; set; }

        public required string Origin { get; set; }


        public required string MakeLogo { get; set; }

        public required bool Active { get; set; }

        public required int YearFounded { get; set; }

        public required string Company { get; set; }

        public required string MakeCode { get; set; }

        public required string MakeAbbreviation { get; set; }

        public DateTime DateCreated { get; set; }

        public ICollection<Model>? Models { get; set; }
    }
}
