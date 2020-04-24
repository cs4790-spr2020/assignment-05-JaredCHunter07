using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlabberApp.DataStore;
using BlabberApp.DataStore.Exceptions;
using BlabberApp.Domain;

namespace BlabberApp.DataStoreTest
{
    [TestClass]
    public class UserAdapter_MySql_UnitTests
    {
        private User _user;
        private UserAdapter _harness = new UserAdapter(new MySqlUser());
        private readonly string _email = "foobar@example.com";

        [TestInitialize]
        public void Setup()
        {
            _user = new User(_email);
        }
        [TestCleanup]
        public void TearDown()
        {
            User user = new User(_email);
            _harness.Remove(user);
        }

        [TestMethod]
        public void Canary()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestAddAndGetUser()
        {
            //Arrange
            _user.RegisterDTTM =DateTime.Now;
            _user.LastLoginDTTM = DateTime.Now;
            //Act
            _harness.Add(_user);
            User actual = _harness.GetById(_user.Id);
            //Assert
            Assert.AreEqual(_user.Id, actual.Id);
        }
        [TestMethod]
        public void TestAddAndGetAll()
        {
            //Arrange
            _user.RegisterDTTM =DateTime.Now;
            _user.LastLoginDTTM = DateTime.Now;
            _harness.Add(_user);
            //Act
            ArrayList users = (ArrayList)_harness.GetAll();
            User actual = (User)users[users.Count - 1];
            //Assert
            Assert.AreEqual(_user.Id.ToString(), actual.Id.ToString());
        }
        [TestMethod]
        public void TestUpdateUser()
        {
            _user.RegisterDTTM = DateTime.Now;
            _user.LastLoginDTTM = DateTime.Now;
            _harness.Add(_user);
            DateTime expected = _user.LastLoginDTTM;
            System.Threading.Thread.Sleep(2000);
            _user.LastLoginDTTM = DateTime.Now;
            _harness.Update(_user);
            Assert.AreNotEqual(expected.ToString(), _user.LastLoginDTTM.ToString());
        }

        [TestMethod]
        public void TestGetByEmail()
        {
            _harness.Add(_user);
            User expected = _harness.GetByEmail(_user.Email);
            Assert.AreEqual(expected.Email, _user.Email);
        }

        [TestMethod]
        public void TestAddUser_Fail00()
        {
            _harness.Add(_user);
            User anotherUser = new User(_email);
            var ex = Assert.ThrowsException<UserAdapterDuplicateException>(() => _harness.Add(anotherUser));

            string message = "Email already exists.";
            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void TestGetById_Fail00()
        {
            _harness.Add(_user);
            Guid testId = new Guid("FFFFFFFF-FFFF-FFFF-FFFF-0123456fdafc");
            var ex = Assert.ThrowsException<UserAdapterNotFoundException>(() => _harness.GetById(testId));

            string message = testId + ": Not found.";

            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void TestUpdate_Fail00()
        {
            User testUser = new User("nonexistent@e.com");
            testUser.RegisterDTTM = DateTime.Now;
            testUser.LastLoginDTTM = DateTime.Now;
            System.Threading.Thread.Sleep(2000);
            testUser.LastLoginDTTM = DateTime.Now;
            var ex = Assert.ThrowsException<UserAdapterException>(() => _harness.Update(testUser));

            string message = "Unable to update user";

            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void TestValid_Fail00()
        {
            User testUser = new User();
            var ex = Assert.ThrowsException<ArgumentNullException>(() => testUser.IsValid());
            //Console.WriteLine(ex.Message.ToString());

            string message = "Value cannot be null.";

            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void TestRegisterDTTM()
        {
            _user.RegisterDTTM = DateTime.Now;
            DateTime now = DateTime.Now;
            _harness.Add(_user);
            ArrayList userList = (ArrayList)_harness.GetAll();
            User testUser = (User)userList[userList.Count - 1];

            Assert.AreEqual(now.ToString(), testUser.RegisterDTTM.ToString());
        }
    }
}