using BrandMicroservice.src.Models.DbModels;
using BrandMicroservice.src.Models.Dtos.Model;
using BrandMicroservice.src.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BrandMicroservice.src.Controllers
{
    [ApiController]
    [Route("api/brand-microservice/model")]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            this._modelService = modelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model>>> GetModels()
        {
            return Ok(await this._modelService.GetModels());
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateModel(ModelRequestBody body)
        {
            return Ok(await this._modelService.CreateModel(body));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Model>> GetModelById(Guid id)
        {
            return Ok(await this._modelService.GetModelById(id));
        }

        [HttpGet("modelsByMakeName/{makeName}")]
        public async Task<ActionResult<IEnumerable<ModelByMakeNameList>>> GetModelsByMakeName(string makeName)
        {
            var res = await this._modelService.GetModelsByMakeName(makeName);
            return Ok(res);
        }
    }
}
