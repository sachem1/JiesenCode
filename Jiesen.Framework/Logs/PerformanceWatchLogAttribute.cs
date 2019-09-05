using System;
using Jiesen.Framework.Common;
using Jiesen.Framework.ConfigManager;
using Newtonsoft.Json;

namespace Jiesen.Framework.Logs
{
    /// <summary>
    /// 性能监控日志输出属性类
    /// </summary>
    public class PerformanceWatchLogAttribute 
    {
        private DateTime _onBeforeDateTime = DateTime.Now;
        private readonly bool _debug = Convert.ToBoolean("True");
        private const string LogTag = "TimeConsuming";
        private static readonly ILogger _logger = LoggerFactory.GetLogger(LogTag);
        

        protected override void OnBefore(IMethodInvocation input)
        {
            if (!_debug) return;
            _onBeforeDateTime = DateTime.Now;
        }

        protected override void OnAfter(IMethodInvocation input)
        {
            if (!_debug) return;
            try
            {
                var currentDateTime = DateTime.Now;
                var ts = currentDateTime.Subtract(_onBeforeDateTime);
                if (input.MethodBase.DeclaringType == null) return;

                _logger.Debug("性能监控->完整方法：{3},方法：{0},参数：{1},耗时：{2}",
                    input.MethodBase.Name, JsonConvert.SerializeObject(input.Arguments), ts.TotalSeconds,input.MethodBase);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}