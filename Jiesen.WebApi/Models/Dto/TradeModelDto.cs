using System;

namespace Epass.Vue.WebApi.Models.Dto
{
    public class TradeModelDto: BaseModel
    {
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string CreateDate { get; set; }=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}