using Elect.Web.HttpDetection.Models;

namespace Elect.Test.Web.HttpDetection
{
    [TestClass]
    public class DeviceTypeUnitTest
    {
        [TestMethod]
        public void DeviceType_Unknown_HasCorrectValue()
        {
            // Act & Assert
            Assert.AreEqual(0, (int)DeviceType.Unknown);
        }

        [TestMethod]
        public void DeviceType_Desktop_HasCorrectValue()
        {
            // Act & Assert
            Assert.AreEqual(1, (int)DeviceType.Desktop);
        }

        [TestMethod]
        public void DeviceType_Tablet_HasCorrectValue()
        {
            // Act & Assert
            Assert.AreEqual(2, (int)DeviceType.Tablet);
        }

        [TestMethod]
        public void DeviceType_Mobile_HasCorrectValue()
        {
            // Act & Assert
            Assert.AreEqual(3, (int)DeviceType.Mobile);
        }

        [TestMethod]
        public void DeviceType_ToString_ReturnsCorrectString()
        {
            // Act & Assert
            Assert.AreEqual("Unknown", DeviceType.Unknown.ToString());
            Assert.AreEqual("Desktop", DeviceType.Desktop.ToString());
            Assert.AreEqual("Tablet", DeviceType.Tablet.ToString());
            Assert.AreEqual("Mobile", DeviceType.Mobile.ToString());
        }

        [TestMethod]
        public void DeviceType_EnumParse_WorksCorrectly()
        {
            // Act & Assert
            Assert.AreEqual(DeviceType.Unknown, Enum.Parse<DeviceType>("Unknown"));
            Assert.AreEqual(DeviceType.Desktop, Enum.Parse<DeviceType>("Desktop"));
            Assert.AreEqual(DeviceType.Tablet, Enum.Parse<DeviceType>("Tablet"));
            Assert.AreEqual(DeviceType.Mobile, Enum.Parse<DeviceType>("Mobile"));
        }

        [TestMethod]
        public void DeviceType_IsDefined_WorksCorrectly()
        {
            // Act & Assert
            Assert.IsTrue(Enum.IsDefined(typeof(DeviceType), DeviceType.Unknown));
            Assert.IsTrue(Enum.IsDefined(typeof(DeviceType), DeviceType.Desktop));
            Assert.IsTrue(Enum.IsDefined(typeof(DeviceType), DeviceType.Tablet));
            Assert.IsTrue(Enum.IsDefined(typeof(DeviceType), DeviceType.Mobile));
        }

        [TestMethod]
        public void DeviceType_GetValues_ReturnsAllValues()
        {
            // Act
            var values = Enum.GetValues<DeviceType>();

            // Assert
            Assert.AreEqual(4, values.Length);
            Assert.IsTrue(values.Contains(DeviceType.Unknown));
            Assert.IsTrue(values.Contains(DeviceType.Desktop));
            Assert.IsTrue(values.Contains(DeviceType.Tablet));
            Assert.IsTrue(values.Contains(DeviceType.Mobile));
        }

        [TestMethod]
        public void DeviceType_GetNames_ReturnsAllNames()
        {
            // Act
            var names = Enum.GetNames<DeviceType>();

            // Assert
            Assert.AreEqual(4, names.Length);
            Assert.IsTrue(names.Contains("Unknown"));
            Assert.IsTrue(names.Contains("Desktop"));
            Assert.IsTrue(names.Contains("Tablet"));
            Assert.IsTrue(names.Contains("Mobile"));
        }
    }
}