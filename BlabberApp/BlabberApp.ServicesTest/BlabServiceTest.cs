using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlabberApp.DataStore;
using BlabberApp.Domain;
using BlabberApp.Services;

namespace BlabberApp.ServicesTest
{
    [TestClass]
    public class BlabServiceTest
    {
        private BlabServiceFactory _blabServiceFactory = new BlabServiceFactory();

        [TestMethod]
        public void TestCanary()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void GetAllEmptyTest()
        {
            //Arrange
            BlabService blabService = _blabServiceFactory.CreateBlabService();
            ArrayList expected = new ArrayList();
            //Act
            IEnumerable actual = blabService.GetAll();
            //Assert
            Assert.AreEqual(expected.Count, (actual as ArrayList).Count);
        }

        [TestMethod]
        public void AddNewBlabTest()
        {
            //Arrange
            string email = "user@example.com";
            string msg = "Prow scuttle parrel provost Sail ho shrouds spirits boom mizzenmast yardarm.";
            BlabService blabService = _blabServiceFactory.CreateBlabService();
            Blab blab = blabService.CreateBlab(msg, email);
            blabService.AddBlab(blab);
            //Act
            //Blab actual = (Blab)blabService.FindUserBlabs(email);
            var actual = Assert.ThrowsException<NotImplementedException>(() => blabService.FindUserBlabs(email));
            //Assert
            Assert.AreEqual("FindUserBlabs", actual.Message);
        }

        [TestMethod]
        public void TestAddAndGetDTTM()
        {
            Blab blab = new Blab("Now is the TIME for blabs");
            DateTime now = DateTime.Now;
            blab.DTTM = DateTime.Now;
            BlabService blabService = _blabServiceFactory.CreateBlabService();
            blabService.AddBlab(blab);

            ArrayList blabList = (ArrayList)blabService.GetAll();
            Blab expected = (Blab)blabList[blabList.Count - 1];

            Assert.AreEqual(expected.DTTM.ToString(), now.ToString());

        }
    }
}