using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlabberApp.DataStore;
using BlabberApp.DataStore.Exceptions;
using BlabberApp.Domain;

namespace BlabberApp.DataStoreTest
{
    [TestClass]
    public class BlabAdapter_MySql_UnitTests
    {
        private BlabAdapter _harness;

        [TestInitialize]
        public void Setup()
        {
            _harness = new BlabAdapter(new MySqlBlab());
        }

        [TestMethod]
        public void Canary()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestAddAndGetBlab()
        {
            //Arrange
            string email = "fooabar@example.com";
            User mockUser = new User(email);
            Blab blab = new Blab("Now is the time for, blabs...", mockUser);
            //Act
            _harness.Add(blab);
            ArrayList actual = (ArrayList)_harness.GetByUserId(email);
            _harness.Remove(blab);
            //Assert
            if (actual.Count == 1)
            {
                Assert.AreEqual(1, actual.Count);
            }
            else {
                Assert.AreNotEqual(1, actual.Count);
            }
        }

        // [TestMethod]
        // public void TestAddAndRemoveBlab()
        // {
        //     //Arrange
        //     string email = "fooabar@example.com";
        //     User mockUser = new User(email);
        //     Blab blab = new Blab("Now is NOT the time for, blabs...", mockUser);
        //     //Act pt 1
        //     _harness.Add(blab);
        //     ArrayList actual = (ArrayList)_harness.GetAll();
        //     Blab expected = (Blab)actual[actual.Count - 1];
        //     Blab actualBlab = expected;
        //     //Assert pt 1
        //     Assert.AreEqual(expected.Id.ToString(), actualBlab.Id.ToString());
        //     //Act pt 2
        //     _harness.Remove(blab);
        //     actual = (ArrayList)_harness.GetAll();
        //     expected = (Blab)actual[actual.Count - 1];
        //     //Assert pt 2
        //     Assert.AreNotEqual(expected.Id.ToString(), actualBlab.Id.ToString());

        // }

        [TestMethod]
        public void TestAddAndUpdateBlab()
        {
            //Arrange
            string email = "fooabar@example.com";
            User mockUser = new User(email);
            Blab blab = new Blab("Now is the time for, blabs...", mockUser);
            string oldMessage = blab.Message;
            //Act pt 1
            _harness.Add(blab);
            string message = "Now is the time for, updated blabs...";
            blab.Message = message;
            _harness.Update(blab);
            Blab expected = _harness.GetById(blab.Id);

            _harness.Remove(blab);
            Assert.AreNotEqual(expected.Message, oldMessage);
        }

        [TestMethod]
        public void TestAddAndGetBlabById()
        {
            //Arrange
            string email = "fooabar@example.com";
            User mockUser = new User(email);
            Blab blab = new Blab("Now is the time for, blabs...");
            blab.User = mockUser;
            //Act
            _harness.Add(blab);
            Blab getBlab = _harness.GetById(blab.Id);
            _harness.Remove(blab);
            //Assert
            Assert.AreEqual(getBlab.Id.ToString(), blab.Id.ToString());
        }

        [TestMethod]
        public void TestGetById_Fail00()
        {
            Guid testId = new Guid("FFFFFFFF-FFFF-FFFF-FFFF-0123456fdafc");
            var ex = Assert.ThrowsException<BlabAdapterNotFoundException>(() => _harness.GetById(testId));

            string message = testId + ": Not found.";

            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void TestAddAndGetDTTM()
        {
            Blab blab = new Blab("Now is the TIME for, blabs...", new User("foobar@example.com"));
            DateTime now = DateTime.Now;
            blab.DTTM = DateTime.Now;

            _harness.Add(blab);
            //Blab expected = _harness.GetById(blab.Id);
            ArrayList blabList = (ArrayList)_harness.GetAll();
            Blab expected = (Blab)blabList[blabList.Count - 1];
            _harness.Remove(blab);

            Assert.AreEqual(expected.DTTM.ToString(), now.ToString());
        }

        [TestMethod]
        public void TestAddAndIsValid()
        {
            Blab blab = new Blab("Now is the time for, valid blabs...", new User("foobar@example.com"));
            _harness.Add(blab);
            Blab expected = _harness.GetById(blab.Id);
            _harness.Remove(blab);

            Assert.AreEqual(expected.IsValid(), blab.IsValid());
        }
    }
}