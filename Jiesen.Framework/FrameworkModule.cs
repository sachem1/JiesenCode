using Autofac;
using Jiesen.ConsoleApp.Strategy;
using Jiesen.Framework.Strategy;

namespace Jiesen.Framework
{
    public class FrameworkModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FiFoMatch>().Keyed<ICalcuation>(MatchType.FiFo);
            builder.RegisterType<NearAveragePrice>().Keyed<ICalcuation>(MatchType.AveragePrice);
        }
    }
}
