using System;
using Jiesen.Framework.Common;
using Jiesen.Framework.ConfigManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jiesen.Framework.Test
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void Sample_Config_Test()
        {
            var zhang = AppConfig.DllConfigs[StaticString.DefaultConfigDll]["Framework"]["zhang"];
            Assert.IsNotNull(zhang);
        }
    }
}
