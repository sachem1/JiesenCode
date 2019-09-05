using System;
using Jiesen.Framework.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jiesen.Framework.Test
{
    [TestClass]
    public class ProgramInitTest
    {
        [TestMethod]
        public void Init_Module()
        {
            ContextBuilder.BuildPlatform().BuildAppContext();
        }
    }
}
