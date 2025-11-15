using BrandMicroservice.src.Data;
using BrandMicroservice.src.Models.DbModels;
using BrandMicroservice.src.Models.Dtos.Model;
using BrandMicroservice.src.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BrandMicroservice.src.Repository
{
    public class ModelRepository : IModelRepository
    {
        public readonly DatabaseContext _dbContext;
        public ModelRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<bool> CreateModelAsync(Model model)
        {
            await this._dbContext.Models.AddAsync(model);
                
            var res = this._dbContext.SaveChanges();
            return true;
        }

        public async Task<bool?> DeleteModelAsync(Guid id)
        {
            var res = await this._dbContext.Models.Where(mo => mo.Id == id).ExecuteDeleteAsync();

            return res > 0 ? true : false;
        }

        async public Task<Model?> FindModelById(Guid id)
        {
            try
            {
                var res = await (from model in this._dbContext.Models
                                 where model.Id == id
                                 select model
                                ).FirstOrDefaultAsync();
                return res;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Model?> FindModelByModelName(string modelName)
        {
            try
            {
                var modelData = await (from model in this._dbContext.Models
                                      where model.ModelName == modelName
                                      select model).FirstOrDefaultAsync();
                return modelData;                                      
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Model>> FindModelsAsync()
        {
            //string orderby = "DateCreated";
            //string asc = "ASC";
            var res = await (from model in this._dbContext.Models
                             join make in this._dbContext.Makes on model.MakeId equals make.Id
                             select new Model()
                             {
                                 Id = model.Id,
                                 ModelName = model.ModelName,
                                 Active = model.Active,
                                 BodyType = model.BodyType,
                                 YearFounded = model.YearFounded,
                                 MakeId = model.MakeId,
                                 ModelCode = model.ModelCode,
                                 Makes = model.Makes,
                             }).ToListAsync();

            return res;
        }

        public async Task<List<ModelByMakeNameList>> GetModelsByMakeName(string makeName)
        {
            //var data = await (from model in this._dbContext.Models
            //                  join make in this._dbContext.Makes on model.MakeId equals make.Id
            //                  where make.MakeName == makeName
            //                  orderby make.MakeName ascending
            //                  select new ModelByMakeNameList { ModelCode = model.ModelCode, ModelName = model.ModelName }).ToListAsync();

            var res = await this._dbContext.Models
                .Where(mo=>mo.Makes != null && mo.Makes.MakeName == makeName)
                .Join(this._dbContext.Makes,
                mo => mo.MakeId,
                ma => ma.Id,
                (mo, ma) => new ModelByMakeNameList { ModelCode = mo.ModelCode, ModelName = mo.ModelName })
                .ToListAsync();
            return res;
        }
    }
}
