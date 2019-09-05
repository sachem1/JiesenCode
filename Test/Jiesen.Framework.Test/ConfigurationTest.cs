using System;
using Jiesen.Core.Common;
using Jiesen.Core.ConfigManager;
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
