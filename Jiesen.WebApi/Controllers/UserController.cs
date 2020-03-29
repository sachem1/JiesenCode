using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Epass.Vue.WebApi.Models;
using Epass.Vue.WebApi.Models.Dto;

namespace Epass.Vue.WebApi.Controllers
{
    public class UserController : BaseController<UserModelDto>
    {
        private static readonly List<UserModelDto> UserList = new List<UserModelDto>();

        [Route("api/user/create")]
        [HttpPost]
        public ReturnResult<UserModelDto> Create(UserModelDto t)
        {
            if (t.Name == null) return new ReturnResult<UserModelDto>() { Status = 1, Message = "用户名不能为空!" };
            UserList.Add(t);
            return GenerateSuccessResult(t);
        }


        [Route("api/user/update")]
        [HttpPost]
        public ReturnResult<UserModelDto> Update(UserModelDto t)
        {
            if (t.Name == null) return new ReturnResult<UserModelDto>() { Status = 1, Message = "用户名不能为空!" };
            UserList.Add(t);
            return GenerateSuccessResult(t);
        }



        [Route("api/user/deleteRang")]
        [HttpPost]
        public ReturnResult<UserModelDto> DeleteRang(IList<long> ids)
        {
            if (!ids.Any()) return new ReturnResult<UserModelDto>() { Status = 1, Message = "请选择要删除的数据!" };
            UserList.RemoveAll(x => ids.Contains(x.Id));
            return GenerateSuccessResult(null);
        }

        [Route("api/user/deleteCondition")]
        [HttpPost]
        public ReturnResult<UserModelDto> DeleteCondition(UserModelDto t)
        {
            if (t.Name == null) return new ReturnResult<UserModelDto>() { Status = 1, Message = "用户名不能为空!" };
            UserList.RemoveAll(x => x.Name == t.Name);
            return GenerateSuccessResult(t);
        }

        [Route("api/user/GetPaged")]
        [HttpPost]
        public ReturnResult<PagedResult<UserModelDto>> GetPaged(UserModelDto tradeSearch)
        {
            if (!UserList.Any())
                InitData();
            if (!string.IsNullOrEmpty(tradeSearch?.Name))
            {
                return new ReturnResult<PagedResult<UserModelDto>>() { Data = new PagedResult<UserModelDto>() { Items = UserList.Where(x => x.Name.Contains(tradeSearch.Name)).ToList(), TotalCount = UserList.Count } };
            }
            return new ReturnResult<PagedResult<UserModelDto>>() { Data = new PagedResult<UserModelDto>() { Items = UserList, TotalCount = UserList.Count } };
        }

        private void InitData()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 20; i++)
            {
                UserModelDto userModel = new UserModelDto
                {
                    Name = "张三" + random.Next(1000),
                    Age = 20 + i,
                    Address = random.Next(1000, 100000).ToString(),
                    LoginName = "dfdf",
                    Password = random.Next(100000, 1000000).ToString(),
                    Id = DateTime.Now.Ticks
                };
                UserList.Add(userModel);
            }
        }


    }
}