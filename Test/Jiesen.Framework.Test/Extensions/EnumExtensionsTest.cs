using System.Collections.Generic;
using Jiesen.Framework.Extensions;
using Jiesen.Framework.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Jiesen.Framework.Test.Extensions
{
    [TestClass]
    public class EnumExtensionsTest
    {
        [TestMethod]
        public void Get_Enum_Description_Test()
        {
            var descriptionList = typeof(NumType).GetEnumDescriptions();
            for (int i = 0; i < descriptionList.Count; i++)
            {
                Assert.IsNotNull(descriptionList[i].Description);
            }
        }

        [TestMethod]
        public void Get_Enum_DataSoure_Test()
        {
            var dataSourceList = typeof(NumType).GetDescriptionToDataSource();
            foreach (var data in dataSourceList)
            {
                Assert.IsNotNull(data.Text);
                Assert.IsNotNull(data.Value);
            }
        }

        [TestMethod]
        public void Get_Enum_Json_Test()
        {
            var json = typeof(NumType).GetDescriptionToDataSourceJson();
            var dataSourceList = JsonConvert.DeserializeObject<List<DataSourceModel>>(json);
            Assert.IsNotNull(dataSourceList);
        }
    }
}
