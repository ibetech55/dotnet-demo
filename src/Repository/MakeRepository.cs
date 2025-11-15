using BrandMicroservice.src.Models.DbModels;
using BrandMicroservice.src.Repository.Interfaces;
using BrandMicroservice.src.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using BrandMicroservice.src.Models.Dtos.Make;

namespace BrandMicroservice.src.Repository
{
    public class MakeRepository : IMakeRepository
    {
        private readonly DatabaseContext _dbContext;
        public MakeRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> CreateMakeAsync(Make make)
        {
            await this._dbContext.Makes.AddAsync(make);
            var res = this._dbContext.SaveChanges();

            return true;
        }

        public async Task<Make?> FindMakeByIdAsync(Guid id)
        {
            try
            {
                var make = await (from ma in this._dbContext.Makes
                           where ma.Id == id
                           select ma).FirstOrDefaultAsync();

                return make;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Make>> FindMakesAsync()
        {
            var res = await (from make in this._dbContext.Makes
                             select new Make()
                             {
                                 Id = make.Id,
                                 MakeName = make.MakeName,
                                 MakeAbbreviation = make.MakeAbbreviation,
                                 MakeCode = make.MakeCode,
                                 MakeLogo = make.MakeLogo,
                                 Active = make.Active,
                                 Company = make.Company,
                                 YearFounded = make.YearFounded,
                                 Origin = make.Origin,
                                 Models = make.Models,
                             }).ToListAsync() ;

            return res;
        }

        public async Task<Make?> FindMakeByMakeNameAsync(string makeName)
        {
            var makeData = await (from make in this._dbContext.Makes
                                  where make.MakeName == makeName
                                  select make
                                 ).FirstOrDefaultAsync();
            return makeData;
        }

        public async Task<List<MakeNameList>>GetMakeNameListAsync()
        {
           var makeNameListData = await (from make in this._dbContext.Makes
                                         orderby make.MakeName ascending
                                         select  new MakeNameList { MakeCode = make.MakeCode, MakeName = make.MakeName }).ToListAsync();
            return makeNameListData;
        }
    }
}
