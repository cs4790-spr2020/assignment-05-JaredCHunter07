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
    public class UserService_MySql_Tests
    {
        private User _user;
        private readonly string _email = "example@foobar.com";
        private static UserServiceFactory userServiceFactory = new UserServiceFactory();
        private static IUserPlugin userPlugin = userServiceFactory.CreateUserPlugin("mysql");
        private static UserAdapter userAdapter = userServiceFactory.CreateUserAdapter(userPlugin);
        private static UserService userService = userServiceFactory.CreateUserService(userAdapter);

        [TestInitialize]
        public void Setup()
        {
            _user = new User(_email);
        }
        [TestCleanup]
        public void TearDown()
        {
            User user = new User(_email);
            userService.RemoveUser(user);
        }

        [TestMethod]
        public void Canary()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestAddAndGetUser()
        {
            userService.AddNewUser(_email);
            User actual = userService.FindUser(_email);

            Assert.AreEqual(_email, actual.Email);
        }

        [TestMethod]
        public void TestAddAndGetUserById()
        {
            userAdapter.Add(_user);
            User actual = userAdapter.GetById(_user.Id);

            Assert.AreEqual(_user.Id, actual.Id);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            userService.AddNewUser(_email);
            User testUser = userService.FindUser(_email);
            DateTime oldDTTM = testUser.LastLoginDTTM;
            System.Threading.Thread.Sleep(2000);
            testUser.LastLoginDTTM = DateTime.Now;

            userService.UpdateUser(testUser);

            Assert.AreNotEqual(oldDTTM.ToString(), userService.FindUser(_email).LastLoginDTTM.ToString());
        }

        [TestMethod]
        public void TestAddAndGetAll()
        {
            userService.AddNewUser(_user.Email);

            ArrayList userList = (ArrayList) userService.GetAll();
            User actual = (User)userList[userList.Count - 1];

            Assert.AreEqual(_user.Email, actual.Email);
        }

        [TestMethod]
        public void TestAddUser_Fail00()
        {
            userService.AddNewUser(_user.Email);
            var ex = Assert.ThrowsException<Exception>(() => userService.AddNewUser(_user.Email));

            string message = "Unable to add user";

            Assert.AreEqual(message, ex.Message.ToString());
        }

        

    }
}