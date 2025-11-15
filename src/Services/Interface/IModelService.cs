using BrandMicroservice.src.Models.DbModels;
using BrandMicroservice.src.Models.Dtos.Model;
using System.Threading.Tasks;

namespace BrandMicroservice.src.Services.Interface
{
    public interface IModelService
    {
        Task<IEnumerable<Model>> GetModels();

        Task<bool> CreateModel(ModelRequestBody model);

        Task<Model> GetModelById(Guid id);

        Task<List<ModelByMakeNameList>> GetModelsByMakeName(string makeName);
    }
}
