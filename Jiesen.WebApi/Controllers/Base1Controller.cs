using System.Collections.Generic;
using System.Web.Http;
using Epass.Vue.WebApi.Models;
using Epass.Vue.WebApi.Models.Enum;
using Jiesen.WebApi.Models;

namespace Epass.Vue.WebApi.Controllers
{
    public class Base1Controller<TEntity, TModel, TGetPagedInput> : ApiController
        where TEntity : BaseEntity
        where TModel : BaseModel
        where TGetPagedInput : BaseSearch
    {
        public ReturnResult<TModel> GenerateSuccessResult(TModel t)
        {
            return new ReturnResult<TModel> { Status = (int)ResultState.Success, Data = t, Message = "请求成功" };
        }

        public ReturnResult<TModel> GenerateFailResult(int code, TModel t, string message)
        {
            return new ReturnResult<TModel> { Status = code, Data = t, Message = message };
        }

        public virtual PagedResult<TModel> GetPaged(BaseSearch search)
        {
            return new PagedResult<TModel>();
        }
    }
}
