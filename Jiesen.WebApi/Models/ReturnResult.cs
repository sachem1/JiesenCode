using System;

namespace Epass.Vue.WebApi.Models
{
    public class ReturnResult<T> where T : BaseModel
    {
        /// <summary>
        /// 0表示正常/成功，非0代表错误码。
        /// </summary>
        public int Status { get; set; }

        public string Message { get; set; }

        public Object Result { get; set; }
    }
}