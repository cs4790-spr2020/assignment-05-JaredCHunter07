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
    public class UserServiceTest
    {
        private UserServiceFactory _userServiceFactory = new UserServiceFactory();

        [TestMethod]
        public void TestCanary()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void GetAllEmptyTest()
        {
            //Arrange
            UserService userService = _userServiceFactory.CreateUserService();
            ArrayList expected = new ArrayList();
            //Act
            IEnumerable actual = userService.GetAll();
            //Assert
            Assert.AreEqual(expected.Count, (actual as ArrayList).Count);
        }

        [TestMethod]
        public void AddNewUserSuccessTest()
        {
            //Arrange
            string email = "user@example.com";
            UserService userService = _userServiceFactory.CreateUserService();
            User expected = userService.CreateUser(email);
            userService.AddNewUser(email);
            //Act
            User actual = userService.FindUser(email);
            //Assert
            Assert.AreEqual(expected.Email, actual.Email);
        }

        [TestMethod]
        public void ReadByEmailTest()
        {
            string email = "foobar@example.com";
            UserService userService = _userServiceFactory.CreateUserService();
            userService.AddNewUser(email);

            User user = userService.FindUser(email);

            Assert.AreEqual(user.Email, email);

        }

        [TestMethod]
        public void ReadByEmailTest_Fail00()
        {
            string email = "non@exis.tent";
            UserService userService = _userServiceFactory.CreateUserService();

            var ex = Assert.ThrowsException<UserAdapterNotFoundException>(() => userService.FindUser(email));

            string message = email + " not found";

            Assert.AreEqual(message, ex.Message.ToString());

        }

        [TestMethod]
        public void AddNewUser_Fail00()
        {
            string email = "";
            UserService userService = _userServiceFactory.CreateUserService();

            var ex = Assert.ThrowsException<Exception>(() => userService.AddNewUser(email));

            string message = "Unable to add user";
            Assert.AreEqual(message, ex.Message.ToString());

        }
        [TestMethod]
        public void AddNewUser_Fail01()
        {
            UserService userService = _userServiceFactory.CreateUserService();
            string email = "emailthatalready@exists.com";
            userService.AddNewUser(email);

            var ex = Assert.ThrowsException<Exception>(() => userService.AddNewUser(email));

            string message = "Unable to add user";
            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void RemoveUserTest()
        {
            UserService userService = _userServiceFactory.CreateUserService();
            string email = "foobar@example.com";
            userService.AddNewUser("some@e.mail");
            userService.AddNewUser(email);
            userService.RemoveUser(userService.FindUser(email));
            ArrayList userList = (ArrayList)userService.GetAll();
            User user = (User)userList[userList.Count - 1];

            Assert.AreNotEqual(user.Email, email);
        }

        [TestMethod]
        public void UpdateUserTest_Fail00()
        {
            UserService userService = _userServiceFactory.CreateUserService();
            string email = "non@exist.ent";
            User testUser = new User(email);

            var ex = Assert.ThrowsException<UserAdapterException>(() => userService.UpdateUser(testUser));

            string message = "Unable to update user";

            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void ChangeEmailTest_Fail00()
        {
            User user = new User("valid@email.com");
            var ex = Assert.ThrowsException<FormatException>(() => user.ChangeEmail("invalidemail"));

            string message = "invalidemail is invalid";

            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void IsValidTest()
        {
            User user = new User("some@email.com");
            bool isValid = user.IsValid();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void IsValidTest_Fail00()
        {
            User user = new User();
            var ex = Assert.ThrowsException<ArgumentNullException>(() => user.IsValid());

            string message = "Value cannot be null.";

            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void RegisterDTTMTest()
        {
            User user = new User("some@email.com");
            UserService userService = _userServiceFactory.CreateUserService();
            userService.AddNewUser(user.Email);

            ArrayList userList = (ArrayList)userService.GetAll();
            User testUser = (User)userList[userList.Count - 1];

            Assert.AreEqual(user.RegisterDTTM.ToString(), testUser.RegisterDTTM.ToString());

        }

        [TestMethod]
        public void ReadByIdTest_Fail00()
        {
            Guid id = new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");
            UserAdapter userAdapter = _userServiceFactory.CreateUserAdapter();
            var ex = Assert.ThrowsException<UserAdapterNotFoundException>(() => userAdapter.GetById(id));

            string message = id + ": Not found.";

            Assert.AreEqual(message, ex.Message.ToString());
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            UserService userService = _userServiceFactory.CreateUserService();
            userService.AddNewUser("some@email.com");
            User user = userService.FindUser("some@email.com");
            DateTime oldDTTM = user.LastLoginDTTM;
            System.Threading.Thread.Sleep(2000);
            user.LastLoginDTTM = DateTime.Now;
            userService.UpdateUser(user);
            User expectedUser = userService.FindUser("some@email.com");
            Assert.AreNotEqual(oldDTTM.ToString(), user.LastLoginDTTM.ToString());
        }
    }
}