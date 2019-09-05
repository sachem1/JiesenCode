using System.Collections.Generic;

namespace Jiesen.Core.Models
{
    public class AppModel
    {
        public long Id { get; set; } = 1;

        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; } = "Jiesen.Acl";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "权限管理平台";
        /// <summary>
        /// 标识
        /// </summary>
        public string Key { get; set; } = "b1484186dc06e9dee884b2a616612f37";
        /// <summary>
        /// 安全码
        /// </summary>
        public string Secret { get; set; } = "3B0C7C38F7AF4D1DACA27C0A7AF82A83";

        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; } = "http://localhost:10004";

        /// <summary>
        /// 类型
        /// </summary>
        public int AppType { get; set; } = 1;

        public List<object> Nodes { get; set; } = new List<object>();
    }
}
