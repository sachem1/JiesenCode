using System.Collections.Generic;
using System.Web.Http;
using Epass.Vue.WebApi.Models;
using Epass.Vue.WebApi.Models.Enum;

namespace Epass.Vue.WebApi.Controllers
{
    public class BaseController<T> : ApiController where T : BaseModel
    {
        public ReturnResult<T> GenerateSuccessResult(T t)
        {
            return new ReturnResult<T> { Status = (int)ResultState.Success, Result = t, Message = "请求成功" };
        }

        public ReturnResult<T> GenerateFailResult(int code, T t, string message)
        {
            return new ReturnResult<T> { Status = code, Result = t, Message = message };
        }

        public virtual PagedResult<T> GetPaged(BaseSearch search)
        {
            return new PagedResult<T>();
        }

       

    }
}
