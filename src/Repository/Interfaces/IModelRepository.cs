using BrandMicroservice.src.Models.DbModels;
using BrandMicroservice.src.Models.Dtos.Model;

namespace BrandMicroservice.src.Repository.Interfaces
{
    public interface IModelRepository
    {
        Task<List<Model>> FindModelsAsync();

        Task<bool> CreateModelAsync(Model model);

        Task<Model?> FindModelById(Guid id);

        Task<Model?> FindModelByModelName(string modelName);

        Task<List<ModelByMakeNameList>> GetModelsByMakeName(string makeName);

        Task<bool?> DeleteModelAsync(Guid id);

    }
}
