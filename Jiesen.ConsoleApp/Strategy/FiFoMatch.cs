using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jiesen.Framework.Strategy;

namespace Jiesen.ConsoleApp.Strategy
{
    public class FiFoMatch : ICalcuation
    {
        public List<string> MatchCollect(List<object> objects)
        {
            objects.Add("FiFoMatch");
            return objects.Select(x => x.ToString()).ToList();
        }
    }
}
