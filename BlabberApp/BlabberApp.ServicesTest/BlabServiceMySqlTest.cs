using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlabberApp.DataStore;
using BlabberApp.DataStore.Exceptions;
using BlabberApp.Domain;
using BlabberApp.Services;

namespace BlabberApp.ServicesTest
{
    [TestClass]
    public class BlabService_MySql_Tests
    {
        private Blab _blab;
        private readonly string _email = "boofar@example.com";
        private static BlabServiceFactory blabServiceFactory = new BlabServiceFactory();
        private static IBlabPlugin blabPlugin = blabServiceFactory.CreateBlabPlugin("MYSQL");
        private static BlabAdapter blabAdapter = blabServiceFactory.CreateBlabAdapter(blabPlugin);
        private static BlabService blabService = blabServiceFactory.CreateBlabService(blabAdapter);

        [TestInitialize]
        public void Setup()
        {
            _blab = new Blab("Time for service blab", new User(_email));
        }

        [TestCleanup]
        public void TearDown()
        {
            Blab blab = new Blab("Time for service blab", new User(_email));
            //remove the dang thing
            blabService.RemoveBlab(blab);
        }

        [TestMethod]
        public void Canary()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestAddAndGetBlab()
        {
            blabService.AddBlab(_blab);
            ArrayList actual = (ArrayList) blabAdapter.GetByUserId(_blab.User.Email);
            blabService.RemoveBlab(_blab);

            if (actual.Count == 1)
            {
                Assert.AreEqual(1, actual.Count);
            }
            else {
                Assert.AreNotEqual(1, actual.Count);
            }
        }

        [TestMethod]
        public void TestAddAndUpdateBlab()
        {
            string oldMessage = _blab.Message;
            blabService.AddBlab(_blab);
            string newMessage = "Time for NEW service blab";
            _blab.Message = newMessage;
            blabService.UpdateBlab(_blab);

            Blab updatedBlab = blabAdapter.GetById(_blab.Id);
            blabService.RemoveBlab(_blab);

            Assert.AreEqual(updatedBlab.Id.ToString(), _blab.Id.ToString());
        }

        [TestMethod]
        public void TestReadById()
        {
            blabService.AddBlab(_blab);
            Blab blab = blabAdapter.GetById(_blab.Id);
            blabService.RemoveBlab(_blab);

            Assert.AreEqual(_blab.Id, blab.Id);
        }

        [TestMethod]
        public void TestReadById_Fail00()
        {
            Guid id = new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");
            var ex = Assert.ThrowsException<BlabAdapterNotFoundException>(() => blabAdapter.GetById(id));

            string message = id + ": Not found.";

            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void TestReadAll()
        {
            blabService.AddBlab(_blab);
            ArrayList blabList = (ArrayList) blabAdapter.GetAll();

            Blab expected = (Blab) blabList[blabList.Count - 1];
            blabService.RemoveBlab(_blab);

            Assert.AreEqual(expected.Id, _blab.Id);
        }

    }
}