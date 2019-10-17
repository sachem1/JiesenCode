using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Epass.Vue.WebApi.Models;
using Epass.Vue.WebApi.Models.Dto;
using Epass.Vue.WebApi.Models.Search;

namespace Epass.Vue.WebApi.Controllers
{
    public class TradeController : BaseController<TradeModelDto>
    {
        public static List<TradeModelDto> TradeModelList = new List<TradeModelDto>();

        // GET: api/Trade/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/tradeService/GetPaged")]
        public PagedResult<TradeModelDto> GetPaged(TradeSearchDto tradeSearch)
        {
            if (!string.IsNullOrEmpty(tradeSearch?.Name))
            {
                return new PagedResult<TradeModelDto>() { Items = TradeModelList.Where(x => x.Name.Contains(tradeSearch.Name)).ToList(), TotalCount = TradeModelList.Count };
            }
            return new PagedResult<TradeModelDto>() { Items = TradeModelList, TotalCount = TradeModelList.Count };
        }

        [Route("api/tradeService/Create")]
        public ReturnResult<TradeModelDto> Create(TradeModelDto model)
        {
            if (string.IsNullOrEmpty(model.Name)) return GenerateFailResult(1, null, "");
            model.Id = DateTime.Now.Ticks.ToString();
            TradeModelList.Add(model);
            return GenerateSuccessResult(model);
        }

        [Route("api/tradeService/Update")]
        [HttpPut]
        public ReturnResult<TradeModelDto> Update(TradeModelDto model)
        {
            var res = TradeModelList.FirstOrDefault(x => x.Id == model.Id);
            if (res != null)
            {
                res.Name = model.Name;
                res.Address = model.Address;
            }
            return GenerateSuccessResult(model);
        }

        [Route("api/tradeService/DeleteRang")]
        [HttpPost]
        public ReturnResult<TradeModelDto> DeleteRang(List<string> ids)
        {
            TradeModelList.RemoveAll(x => ids.Contains(x.Id));
            return GenerateSuccessResult(null);
        }

        [Route("api/tradeService/Delete")]
        [HttpDelete]
        public ReturnResult<TradeModelDto> Delete(string id)
        {
            TradeModelList.RemoveAll(x => id == x.Id);
            return GenerateSuccessResult(null);
        }

        [Route("api/tradeService/getCityList")]

        public ReturnResult<List<SelectItem>> GetCityList()
        {
            return new ReturnResult<List<SelectItem>>()
            {
                Result = new List<SelectItem>()
                {
                    new SelectItem(){label="北京市",value="1010",parentValue="010"},
                    new SelectItem(){label="上海市",value="1021",parentValue="021"},
                    new SelectItem(){label="广州市",value="1030",parentValue="030"},
                    new SelectItem(){label="深圳市",value="1031",parentValue="030"},
                    new SelectItem(){label="珠海市",value="1032",parentValue="030"}
                },
                Status = 0
            };
        }

        [Route("api/tradeService/getProvinceList")]
        public ReturnResult<List<SelectItem>> GetProvinceList()
        {
            return new ReturnResult<List<SelectItem>>()
            {
                Result = new List<SelectItem>()
                {
                    new SelectItem(){label="北京",value="010"},
                    new SelectItem(){label="上海",value="021"},
                    new SelectItem(){label="广东省",value="030"}
                },
                Status = 0
            };
        }
    }
}
