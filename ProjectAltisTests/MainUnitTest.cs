using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectAltis.Core;
using ProjectAltis.Manifests;
using ProjectAltis.Forms;
namespace ProjectAltisTests
{
    [TestClass]
    public class MainUnitTest
    {
        [TestMethod]
        public void TestLoginApiWorks()
        {
            LoginApiResponse response = Data.GetLoginAPIResponse("drewdev", "drewdev");
            Assert.IsTrue(response.status == "true");
        }
    }
}
