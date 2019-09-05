using System;
using Newtonsoft.Json;

namespace Jiesen.Framework.Logs
{
    /// <summary>
    /// 函数执行耗时日志记录，秒为单位
    /// </summary>
    public class TimeConsumingLogAttribute 
    {
        private DateTime _onBeforeDateTime = DateTime.Now;
        private static readonly TaskQueue Queue = TaskQueue.GetQueue(1);
        private readonly bool _debug = Convert.ToBoolean(AppConfig.DllConfigs.Current[StaticString.LogContextString][StaticString.LogEnableTraceTimeConsumingString] ?? "True");

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

                Queue.Execute(() =>
                {
                    LoggerFactory.GetLogger("TimeConsuming").Info("接口：{0},方法：{1},参数：{2},耗时：{3}", input.MethodBase.DeclaringType.FullName,
                        input.MethodBase.Name, JsonConvert.SerializeObject(input.Arguments), ts.TotalSeconds);
                    return true;
                });
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
