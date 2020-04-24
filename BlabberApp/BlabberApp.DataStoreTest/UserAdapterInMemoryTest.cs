using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlabberApp.Domain;
using BlabberApp.DataStore;
using BlabberApp.DataStore.Exceptions;
using System;
using System.Collections;

namespace BlabberApp.DataStoreTest
{
    [TestClass]
    public class UserAdapter_InMemory_UnitTests 
    {
        private UserAdapter _harness = new UserAdapter(new InMemory());

        [TestMethod]
        public void Canary()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestAddAndGetUserId()
        {
            User user = new User("foobar@example.com");
            user.RegisterDTTM = DateTime.Now;
            user.LastLoginDTTM = DateTime.Now;

            _harness.Add(user);

            User testUser = _harness.GetById(user.Id);

            Assert.AreEqual(testUser.Id.ToString(), user.Id.ToString());
        }

        [TestMethod]
        public void TestReadAll()
        {
            User user = new User("foobar@example.com");
            user.RegisterDTTM = DateTime.Now;
            user.LastLoginDTTM = DateTime.Now;

            _harness.Add(user);

            ArrayList userList = (ArrayList)_harness.GetAll();

            User expected = (User)userList[userList.Count - 1];

            Assert.AreEqual(expected.Email, user.Email);
        }

        [TestMethod]
        public void TestReadByUserEmail()
        {
            string email = "foobar@example.com";
            User user = new User(email);
            user.RegisterDTTM = DateTime.Now;
            user.LastLoginDTTM = DateTime.Now;

            _harness.Add(user);

            User expected = _harness.GetByEmail(user.Email);

            Assert.AreEqual(expected.Email, user.Email);
        }

        [TestMethod]
        public void TestReadById_Fail00()
        {
            Guid id = new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");
            var ex = Assert.ThrowsException<UserAdapterNotFoundException>(() => _harness.GetById(id));
            string message = id.ToString() + ": Not found.";

            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void TestReadById()
        {
            User user = new User("foobar@example.com");
            _harness.Add(user);

            User expected = _harness.GetById(user.Id);

            Assert.AreEqual(expected.Id, user.Id);
        }

        [TestMethod]
        public void TestChangeEmail_Fail00()
        {
            User user = new User();
            string email = "this is invalid";
            var ex = Assert.ThrowsException<FormatException>(() => user.ChangeEmail(email));

            string message = email + " is invalid";
            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void TestIsValid()
        {
            User user = new User("foobar@example.com");
            user.RegisterDTTM = DateTime.Now;
            user.LastLoginDTTM = DateTime.Now;

            bool isValid = user.IsValid();

            Assert.AreEqual(true, isValid);

        }

        [TestMethod]
        public void TestUpdate()
        {
            User user = new User("foobar@example.com");
            Guid id = user.Id;
            _harness.Add(user);
            user.ChangeEmail("barfoo@example.com");
            _harness.Update(user);
            User expected = _harness.GetByEmail(user.Email);

            Assert.AreEqual(id, expected.Id);
        }

        [TestMethod]
        public void TestChangeEmail_Fail01()
        {
            User user = new User("foobar@example.com");
            var ex = Assert.ThrowsException<FormatException>(() => user.ChangeEmail(null));
            string message = "Email is invalid";
            Assert.AreEqual(message, ex.Message.ToString());
        }
    }
}