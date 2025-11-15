using BrandMicroservice.src.Models.DbModels;
using BrandMicroservice.src.Models.Dtos.Make;

namespace BrandMicroservice.src.Services.Interface
{
    public interface IMakeService
    {
        Task<List<Make>> GetMakes();

        Task<bool> CreateMake(MakeRequestBody body);

        Task<Make> GetMakeById(Guid id);

        Task<Make> GetMakeByMakeName(string makeName);

        Task<List<MakeNameList>> GetMakeNameListAsync();
    }
}
