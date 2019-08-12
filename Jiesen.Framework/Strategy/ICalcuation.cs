using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiesen.Framework.Strategy
{
    public interface ICalcuation:IStrategy
    {
        List<string> MatchCollect(List<object> objects);
    }
}
