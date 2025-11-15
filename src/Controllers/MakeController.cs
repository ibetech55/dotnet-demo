using BrandMicroservice.src.Models.DbModels;
using BrandMicroservice.src.Models.Dtos.Make;
using BrandMicroservice.src.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BrandMicroservice.src.Controllers
{
    [ApiController]
    [Route("api/brand-microservice/make")]
    public class MakeController : ControllerBase
    {
        private readonly IMakeService _makeService;
        public MakeController(IMakeService makeService)
        {
            this._makeService = makeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        {
            var res = await this._makeService.GetMakes();
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateMake(MakeRequestBody make)
        {
            var res = await this._makeService.CreateMake(make);
            return res == true ? Ok(res) : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Make>> GetMakeById(Guid id)
        {
            Make make = await this._makeService.GetMakeById(id);
            return Ok(make);
        }

        [HttpGet("makeName/{makeName}")]
        public async Task<ActionResult<Make>> GetMakeByMakeName(string makeName)
        {
            Make make = await this._makeService.GetMakeByMakeName(makeName);
            return Ok(make);
        }

        [HttpGet("makeNameList")]
        public async Task<ActionResult<List<MakeNameList>>> GetMakeNameList()
        {
            var data = await this._makeService.GetMakeNameListAsync();

            return Ok(data);
        }
    }
}
