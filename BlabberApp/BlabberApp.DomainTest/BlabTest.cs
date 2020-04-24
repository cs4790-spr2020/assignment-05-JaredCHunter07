using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlabberApp.Domain;

namespace BlabberApp.DomainTest
{
    [TestClass]
    public class BlabTest
    {       
        private Blab harness;
        public BlabTest() 
        {
            harness = new Blab();
        }
        [TestMethod]
        public void TestSetGetMessage()
        {
            // Arrange
            string expected = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit..."; 
            harness.Message = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...";
            // Act
            string actual = harness.Message;
            // Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestId()
        {
            // Arrange
            Guid expected = harness.Id;
            // Act
            Guid actual = harness.Id;
            // Assert
            Assert.AreEqual(actual, expected);
            Assert.AreEqual(true, harness.Id is Guid);
        }
        
        [TestMethod]
        public void TestDTTM()
        {
            // Arrange
            Blab Expected = new Blab();
            // Act
            Blab Actual = new Blab();
            // Assert
            Assert.AreEqual(Expected.DTTM.ToString(), Actual.DTTM.ToString());
        }

        [TestMethod]
        public void TestMessage()
        {
            Blab testBlab = new Blab("Test message");
            string expected = "Test message";
            Assert.AreEqual(expected, testBlab.Message);
        }

        [TestMethod]
        public void TestUser()
        {
            User testUser = new User();
            Blab testBlab = new Blab(testUser);
            Assert.AreEqual(testUser, testBlab.User);
        }

        [TestMethod]
        public void TestSetGetUser()
        {
            User testUser = new User();
            Blab testBlab = new Blab();
            testBlab.User = testUser;
            Assert.AreEqual(testUser, testBlab.User);
        }

        [TestMethod]
        public void TestMessageUser()
        {
            User testUser = new User();
            Blab testBlab = new Blab("Test message", testUser);
            string expected = "Test message";

            Assert.AreEqual(expected, testBlab.Message);
            Assert.AreEqual(testUser, testBlab.User);
        }
    }
}