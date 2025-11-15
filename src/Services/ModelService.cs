using BrandMicroservice.src.Models.DbModels;
using BrandMicroservice.src.Models.Dtos.Model;
using BrandMicroservice.src.Repository.Interfaces;
using BrandMicroservice.src.Services.Interface;

namespace BrandMicroservice.src.Services
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMakeRepository _makeRepository;
        public ModelService(IModelRepository modelRepository, IMakeRepository makeRepository)
        {
            this._modelRepository = modelRepository;
            this._makeRepository = makeRepository;
        }
        public async Task<bool> CreateModel(ModelRequestBody body)
        {
            Make? make = await this._makeRepository.FindMakeByIdAsync(body.MakeId);

            if(make == null)
            {
                throw new ArgumentException("Make id does not exist");
            }

            Model newModel = new Model()
            {
                ModelName = body.ModelName,
                MakeId = make.Id,
                Active = false,
                BodyType = body.BodyType,
                YearFounded = body.YearFounded,
                ModelCode = "Test-Code"
            };

            return await this._modelRepository.CreateModelAsync(newModel);
        }

        public async Task<IEnumerable<Model>> GetModels()
        {
            return await this._modelRepository.FindModelsAsync();
        }

        public async Task<Model> GetModelById(Guid id)
        {
            var res = await this._modelRepository.FindModelById(id);

            if (res == null) 
            {
                throw new ArgumentException("Model not found");
            }

            return res;
        }

        public async Task<List<ModelByMakeNameList>> GetModelsByMakeName(string makeName)
        {
            var makeData = await this._makeRepository.FindMakeByMakeNameAsync(makeName);

            if(makeData == null)
            {
                throw new ArgumentException($"make name {makeName} does not exist");
            }
            var data = await this._modelRepository.GetModelsByMakeName(makeName);

            return data;
        }
    }
}
