using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiesen.Caching.Redis
{
    public class RedisSetting : ICloneable
    {
        public string ClientName { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 默认数据库名称
        /// </summary>
        public int? DefaultDb { get; set; }

        /// <summary>
        /// 服务器列表
        /// </summary>
        public string ServerList { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SyncTimeout { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
