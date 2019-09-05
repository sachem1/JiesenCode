using Jiesen.Caching;
using Jiesen.Core.Common;
using Jiesen.Core.ConfigManager;
using Jiesen.Core.Models;

namespace Jiesen.Framework.Contexts
{
    public static class ContextBuilder
    {
        public static PlatformContext BuildPlatform()
        {
            var cache = CacheFactory.GetRedisCache(sectionName: StaticString.PlatformContextString);
            var result = PlatformContext.Current = new PlatformContext("Caad", cache);

            return result;
        }

        public static AppContext BuildAppContext(this PlatformContext platformContext)
        {
            var app = new AppModel
            {
                Id = JiesenConvert.Convert<long>(AppConfig.DllConfigs["Jiesen.Framework"]["Servers"]["AppId"]),
                Key = AppConfig.DllConfigs["Jiesen.Framework"]["Servers"]["AppKey"],
                Secret = AppConfig.DllConfigs["Jiesen.Framework"]["Servers"]["AppSecret"],
                Name = AppConfig.DllConfigs["Jiesen.Framework"]["Servers"]["AppName"],
                Code = AppConfig.DllConfigs["Jiesen.Framework"]["Servers"]["AppCode"],
                Domain = AppConfig.IsExists("Jiesen.Framework") ?AppConfig.DllConfigs["Jiesen.Framework"]["Servers"]["AppDomain"]:"",
            };

            //if (string.IsNullOrEmpty(app.Domain))
            //{
            //    throw new ArgumentNullException("Domain", "请在Configs文件夹中Host配置文件的Servers节点配置AppDomain项的值");
            //}
            return platformContext.CreateAppContext(app);
        }
    }
}