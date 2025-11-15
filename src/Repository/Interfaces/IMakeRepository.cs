using BrandMicroservice.src.Models.DbModels;
using BrandMicroservice.src.Models.Dtos.Make;

namespace BrandMicroservice.src.Repository.Interfaces
{
    public interface IMakeRepository
    {
        Task<List<Make>> FindMakesAsync();
        Task<bool> CreateMakeAsync(Make make);

        Task<Make?> FindMakeByIdAsync(Guid id);

        Task<Make?> FindMakeByMakeNameAsync(string name);

        Task<List<MakeNameList>> GetMakeNameListAsync();
    }
}
