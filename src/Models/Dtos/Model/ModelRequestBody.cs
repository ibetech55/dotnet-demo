namespace BrandMicroservice.src.Models.Dtos.Model
{
    public class ModelRequestBody
    {
        public required string ModelName { get; set; }

        public required Guid MakeId { get; set; }

        public required string BodyType { get; set; }

        public required string YearFounded { get; set; }
    }
}