using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Jiesen.Core.Common;
using Jiesen.Core.ConfigManager;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Jiesen.Caching.Redis
{
    /// <summary>
    /// redis 管理
    /// </summary>
    public class RedisManager
    {
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static ConcurrentDictionary<string, RedisSetting> _settings = new ConcurrentDictionary<string, RedisSetting>();
        /// <summary>
        /// redis链接信息
        /// </summary>
        private static ConcurrentDictionary<string, ConnectionMultiplexer> _connnections = new ConcurrentDictionary<string, ConnectionMultiplexer>();


        private static ConcurrentDictionary<string, Task<ConnectionMultiplexer>> _connectionMultiplexers = new ConcurrentDictionary<string, Task<ConnectionMultiplexer>>();
        private static TextWriter _log;

        public static void SetRedisLog(TextWriter writer)
        {
            _log = writer;
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// 加锁同步执行
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IDatabase GetClient(RedisSetting setting)
        {
            var config = _settings.GetOrAdd(setting.ClientName, item => setting);
            var connection = _connnections.AddOrUpdate(setting.ClientName, key =>
                            {
                                return ConnectionMultiplexer.Connect(Convert(config), _log);
                            },
                            (key, old) =>
                            {
                                if (!old.IsConnected)
                                {
                                    old.Close();
                                    old.Dispose();
                                    old = ConnectionMultiplexer.Connect(Convert(config), _log);

                                }
                                return old;
                            });
            return connection.GetDatabase();
        }

        //public static Task<ConnectionMultiplexer> GetClientAsync(RedisSetting setting)
        //{
        //    _connectionMultiplexers.AddOrUpdate(setting.ClientName,key =>
        //    {
        //        return ConnectionMultiplexer.ConnectAsync(Convert(setting));
        //    });
        //}

        private static ConfigurationOptions Convert(RedisSetting setting)
        {
            string[] writeServerList = setting.ServerList.Split(',');
            var sdkOptions = new ConfigurationOptions
            {
                ClientName = setting.ClientName,
                KeepAlive = 5 * 60,
                DefaultVersion = new Version(setting.Version),
                AbortOnConnectFail = false,
                DefaultDatabase = setting.DefaultDb,
                ConnectRetry = 10,
                Password = setting.Password,
                SyncTimeout =  setting.SyncTimeout*1000
            };
            foreach (var s in writeServerList)
            {
                sdkOptions.EndPoints.Add(s);
            };
            return sdkOptions;
        }

        

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="dllName"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static RedisSetting GetSetting(string dllName = StaticString.DefaultConfigDll, string sectionName = StaticString.DefautlRedisConfigSection)
        {
            if (!AppConfig.IsExists(dllName))
                throw new FileNotFoundException("未找到对应的配置文件");
            if (!AppConfig.DllConfigs[dllName].IsExists(sectionName))
                throw new FileNotFoundException("未找到对应的配置文件下的配置节点");
            var setting = new RedisSetting();
            try
            {
                setting.ClientName = FormatKey(dllName, sectionName);
                setting.ServerList = AppConfig.DllConfigs[dllName][sectionName]["ServerList"];
                setting.Version = AppConfig.DllConfigs[dllName][sectionName]["Version"];
                setting.DefaultDb = JiesenConvert.Convert<int>(AppConfig.DllConfigs[dllName][sectionName]["DefaultDb"] ?? "0");
                setting.Password = AppConfig.DllConfigs[dllName][sectionName]["Password"];
                setting.SyncTimeout = JiesenConvert.Convert<int>(AppConfig.DllConfigs[dllName][sectionName]["SyncTimeout"] ?? "10000");
                return setting;
            }
            catch (Exception ex)
            {
                throw new Exception($"获取redis配置异常.param:{JsonConvert.SerializeObject(setting)}" , ex);
            }
        }

        public static string FormatKey(params string[] keys)
        {
            return string.Join("-", keys);
        }

    }
}
