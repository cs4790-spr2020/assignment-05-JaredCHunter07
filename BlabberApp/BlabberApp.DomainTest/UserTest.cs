using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlabberApp.Domain;

namespace BlabberApp.DomainTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestSetGetEmail_Success()
        {
            // Arrange
            User harness = new User(); 
            string expected = "foobar@example.com";
            harness.ChangeEmail("foobar@example.com");
            // Act
            string actual = harness.Email; // Assert
            Assert.AreEqual(actual.ToString(), expected.ToString());
        }
        [TestMethod]
        public void TestSetGetEmail_Fail00()
        {
            // Arrange
            User harness = new User(); 
            
            // Act
            var ex = Assert.ThrowsException<FormatException>(() => harness.ChangeEmail("Foobar"));
            string message = "Foobar is invalid";
            // Assert
            Assert.AreEqual(message, ex.Message.ToString());
        }
        [TestMethod]
        public void TestSetGetEmail_Fail01()
        {
            // Arrange
            User harness = new User(); 
            // Act
            var ex = Assert.ThrowsException<FormatException>(() => harness.ChangeEmail("example.com"));
            string message = "example.com is invalid";
            // Assert
            Assert.AreEqual(message, ex.Message.ToString());
        }
        [TestMethod]
        public void TestSetGetEmail_Fail02()
        {
            // Arrange
            User harness = new User(); 
            // Act
            var ex = Assert.ThrowsException<FormatException>(() => harness.ChangeEmail("foobar.example"));
            string message = "foobar.example is invalid";
            // Assert
            Assert.AreEqual(message, ex.Message.ToString());
        }
        [TestMethod]
        public void TestSetGetEmail_Fail03()
        {
            // Arrange
            User harness = new User(); 
            // Act
            var ex = Assert.ThrowsException<FormatException>(() => harness.ChangeEmail(""));
            string message = "Email is invalid";
            // Assert
            Assert.AreEqual(message, ex.Message.ToString());
        }
        [TestMethod]
        public void TestId()
        {
            // Arrange
            User harness = new User();
            Guid expected = harness.Id;
            // Act
            Guid actual = harness.Id;
            // Assert
            Assert.AreEqual(actual, expected);
            Assert.AreEqual(true, harness.Id is Guid);
        }

        [TestMethod]
        public void TestRegisterDTTM()
        {
            User harness = new User();
            harness.RegisterDTTM = DateTime.Now;
            DateTime expected = DateTime.Now;

            Assert.AreEqual(expected.ToString(), harness.RegisterDTTM.ToString());
        }

        [TestMethod]
        public void TestLastLoginDTTM()
        {
            User harness = new User();
            harness.LastLoginDTTM = DateTime.Now;
            DateTime expected = DateTime.Now;

            Assert.AreEqual(expected.ToString(), harness.LastLoginDTTM.ToString());
        }

        [TestMethod]
        public void TestEmail()
        {
            User harness = new User("foobar@example.com");
            string expected = "foobar@example.com";
            Assert.AreEqual(expected, harness.Email);
        }
    }
}