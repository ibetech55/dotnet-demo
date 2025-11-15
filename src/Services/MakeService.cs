using BrandMicroservice.src.Models.DbModels;
using BrandMicroservice.src.Models.Dtos.Make;
using BrandMicroservice.src.Repository.Interfaces;
using BrandMicroservice.src.Services.Interface;
using BrandMicroservice.Utils;

namespace BrandMicroservice.src.Services
{
    public class MakeService : IMakeService
    {
        private readonly IMakeRepository _makeRepository;
        public MakeService(IMakeRepository makeRepository)
        {
            this._makeRepository = makeRepository;
        }
        public  async Task<bool> CreateMake(MakeRequestBody body)
        {
            var makeData = await this._makeRepository.FindMakeByMakeNameAsync(body.MakeName);

            if (makeData != null)
            {
                throw new ArgumentException($"{body.MakeName} already exsits");
            }
            string makeAbbv = body.MakeAbbreviation.ToUpper();
            var newMake = new Make() 
            { 
                MakeName = body.MakeName,
                Origin = body.Origin,
                MakeLogo = body.MakeLogo,
                Active = false,
                YearFounded = body.YearFounded,
                MakeCode = this.GenerateMakeCode(makeAbbv),
                MakeAbbreviation = makeAbbv,
                Company = body.Company,
            };
            var res = await this._makeRepository.CreateMakeAsync(newMake);

            return res;
        }

        public async Task<Make> GetMakeById(Guid id)
        {
            Make? make = await this._makeRepository.FindMakeByIdAsync(id);
            if (make == null)
            {
                throw new KeyNotFoundException("Make not found");
            }
            
            return make;
        }

        public async Task<Make> GetMakeByMakeName(string makeName)
        {
            var res = await this._makeRepository.FindMakeByMakeNameAsync(makeName);

            if(res == null)
            {
                throw new ArgumentException("Model not found");
            }

            return res;
        }

        public async Task<List<MakeNameList>> GetMakeNameListAsync()
        {
            var res = await this._makeRepository.GetMakeNameListAsync();

            return res;
        }

        public async Task<List<Make>> GetMakes()
        {
            return await this._makeRepository.FindMakesAsync();
        }

        private string GenerateMakeCode(string makeAbbv)
        {
            return string.Format($"{makeAbbv}-{GenerateCode.Execute(8)}"); 
        }
    }
}
