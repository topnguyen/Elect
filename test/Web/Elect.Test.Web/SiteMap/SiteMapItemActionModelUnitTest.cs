using Elect.Web.SiteMap.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Elect.Test.Web.SiteMap
{
    [TestClass]
    public class SiteMapItemActionModelUnitTest
    {
        [TestMethod]
        public void Constructor_DefaultValues_AreDefaultForTypes()
        {
            // Act
            var model = new SiteMapItemActionModel();

            // Assert
            Assert.IsNull(model.Action);
            Assert.IsNull(model.Controller);
            Assert.AreEqual(0.0, model.Priority);
            Assert.AreEqual(SiteMapItemFrequency.Never, model.Frequency);
        }

        [TestMethod]
        public void Action_CanBeSetAndGet()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var methodInfo = typeof(TestController).GetMethod("TestAction");

            // Act
            model.Action = methodInfo;

            // Assert
            Assert.AreEqual(methodInfo, model.Action);
        }

        [TestMethod]
        public void Controller_CanBeSetAndGet()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var controllerType = typeof(TestController);

            // Act
            model.Controller = controllerType;

            // Assert
            Assert.AreEqual(controllerType, model.Controller);
        }

        [TestMethod]
        public void Priority_CanBeSetAndGet()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var priority = 0.8;

            // Act
            model.Priority = priority;

            // Assert
            Assert.AreEqual(priority, model.Priority);
        }

        [TestMethod]
        public void Frequency_CanBeSetAndGet()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var frequency = SiteMapItemFrequency.Daily;

            // Act
            model.Frequency = frequency;

            // Assert
            Assert.AreEqual(frequency, model.Frequency);
        }

        [TestMethod]
        public void AllProperties_CanBeSetTogether()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var methodInfo = typeof(TestController).GetMethod("TestAction");
            var controllerType = typeof(TestController);
            var priority = 0.9;
            var frequency = SiteMapItemFrequency.Weekly;

            // Act
            model.Action = methodInfo;
            model.Controller = controllerType;
            model.Priority = priority;
            model.Frequency = frequency;

            // Assert
            Assert.AreEqual(methodInfo, model.Action);
            Assert.AreEqual(controllerType, model.Controller);
            Assert.AreEqual(priority, model.Priority);
            Assert.AreEqual(frequency, model.Frequency);
        }

        [TestMethod]
        public void Priority_CanBeSetToZero()
        {
            // Arrange
            var model = new SiteMapItemActionModel();

            // Act
            model.Priority = 0.0;

            // Assert
            Assert.AreEqual(0.0, model.Priority);
        }

        [TestMethod]
        public void Priority_CanBeSetToOne()
        {
            // Arrange
            var model = new SiteMapItemActionModel();

            // Act
            model.Priority = 1.0;

            // Assert
            Assert.AreEqual(1.0, model.Priority);
        }

        [TestMethod]
        public void Priority_CanBeSetToNegativeValue()
        {
            // Arrange
            var model = new SiteMapItemActionModel();

            // Act
            model.Priority = -0.5;

            // Assert
            Assert.AreEqual(-0.5, model.Priority);
        }

        [TestMethod]
        public void Priority_CanBeSetToValueGreaterThanOne()
        {
            // Arrange
            var model = new SiteMapItemActionModel();

            // Act
            model.Priority = 1.5;

            // Assert
            Assert.AreEqual(1.5, model.Priority);
        }

        [TestMethod]
        public void Frequency_CanBeSetToAllEnumValues()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var allFrequencies = Enum.GetValues(typeof(SiteMapItemFrequency)).Cast<SiteMapItemFrequency>();

            foreach (var frequency in allFrequencies)
            {
                // Act
                model.Frequency = frequency;

                // Assert
                Assert.AreEqual(frequency, model.Frequency);
            }
        }

        [TestMethod]
        public void Action_CanBeSetToNull()
        {
            // Arrange
            var model = new SiteMapItemActionModel
            {
                Action = typeof(TestController).GetMethod("TestAction")
            };

            // Act
            model.Action = null;

            // Assert
            Assert.IsNull(model.Action);
        }

        [TestMethod]
        public void Controller_CanBeSetToNull()
        {
            // Arrange
            var model = new SiteMapItemActionModel
            {
                Controller = typeof(TestController)
            };

            // Act
            model.Controller = null;

            // Assert
            Assert.IsNull(model.Controller);
        }

        [TestMethod]
        public void Action_WithStaticMethod_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var staticMethod = typeof(TestController).GetMethod("StaticTestAction");

            // Act
            model.Action = staticMethod;

            // Assert
            Assert.AreEqual(staticMethod, model.Action);
            Assert.IsTrue(model.Action.IsStatic);
        }

        [TestMethod]
        public void Action_WithPrivateMethod_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var privateMethod = typeof(TestController).GetMethod("PrivateTestAction", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            model.Action = privateMethod;

            // Assert
            Assert.AreEqual(privateMethod, model.Action);
            Assert.IsTrue(model.Action.IsPrivate);
        }

        [TestMethod]
        public void Controller_WithNonControllerType_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var nonControllerType = typeof(string);

            // Act
            model.Controller = nonControllerType;

            // Assert
            Assert.AreEqual(nonControllerType, model.Controller);
        }

        [TestMethod]
        public void Controller_WithAbstractType_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var abstractType = typeof(Controller);

            // Act
            model.Controller = abstractType;

            // Assert
            Assert.AreEqual(abstractType, model.Controller);
        }

        [TestMethod]
        public void Model_InheritsFromElectDisposableModel()
        {
            // Arrange & Act
            var model = new SiteMapItemActionModel();

            // Assert
            Assert.IsInstanceOfType(model, typeof(ElectDisposableModel));
        }

        [TestMethod]
        public void ObjectInitializer_WorksCorrectly()
        {
            // Arrange
            var methodInfo = typeof(TestController).GetMethod("TestAction");
            var controllerType = typeof(TestController);

            // Act
            var model = new SiteMapItemActionModel
            {
                Action = methodInfo,
                Controller = controllerType,
                Priority = 0.7,
                Frequency = SiteMapItemFrequency.Monthly
            };

            // Assert
            Assert.AreEqual(methodInfo, model.Action);
            Assert.AreEqual(controllerType, model.Controller);
            Assert.AreEqual(0.7, model.Priority);
            Assert.AreEqual(SiteMapItemFrequency.Monthly, model.Frequency);
        }

        [TestMethod]
        public void Properties_CanBeModifiedMultipleTimes()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var method1 = typeof(TestController).GetMethod("TestAction");
            var method2 = typeof(TestController).GetMethod("AnotherTestAction");

            // Act & Assert - First assignment
            model.Action = method1;
            Assert.AreEqual(method1, model.Action);

            // Act & Assert - Second assignment
            model.Action = method2;
            Assert.AreEqual(method2, model.Action);

            // Act & Assert - Priority changes
            model.Priority = 0.5;
            Assert.AreEqual(0.5, model.Priority);

            model.Priority = 0.9;
            Assert.AreEqual(0.9, model.Priority);
        }

        [TestMethod]
        public void Properties_AllHavePublicGetters()
        {
            // Arrange
            var type = typeof(SiteMapItemActionModel);

            // Act & Assert
            var actionProperty = type.GetProperty("Action");
            Assert.IsNotNull(actionProperty);
            Assert.IsTrue(actionProperty.CanRead);
            Assert.IsTrue(actionProperty.GetMethod.IsPublic);

            var controllerProperty = type.GetProperty("Controller");
            Assert.IsNotNull(controllerProperty);
            Assert.IsTrue(controllerProperty.CanRead);
            Assert.IsTrue(controllerProperty.GetMethod.IsPublic);

            var priorityProperty = type.GetProperty("Priority");
            Assert.IsNotNull(priorityProperty);
            Assert.IsTrue(priorityProperty.CanRead);
            Assert.IsTrue(priorityProperty.GetMethod.IsPublic);

            var frequencyProperty = type.GetProperty("Frequency");
            Assert.IsNotNull(frequencyProperty);
            Assert.IsTrue(frequencyProperty.CanRead);
            Assert.IsTrue(frequencyProperty.GetMethod.IsPublic);
        }

        [TestMethod]
        public void Properties_AllHavePublicSetters()
        {
            // Arrange
            var type = typeof(SiteMapItemActionModel);

            // Act & Assert
            var actionProperty = type.GetProperty("Action");
            Assert.IsNotNull(actionProperty);
            Assert.IsTrue(actionProperty.CanWrite);
            Assert.IsTrue(actionProperty.SetMethod.IsPublic);

            var controllerProperty = type.GetProperty("Controller");
            Assert.IsNotNull(controllerProperty);
            Assert.IsTrue(controllerProperty.CanWrite);
            Assert.IsTrue(controllerProperty.SetMethod.IsPublic);

            var priorityProperty = type.GetProperty("Priority");
            Assert.IsNotNull(priorityProperty);
            Assert.IsTrue(priorityProperty.CanWrite);
            Assert.IsTrue(priorityProperty.SetMethod.IsPublic);

            var frequencyProperty = type.GetProperty("Frequency");
            Assert.IsNotNull(frequencyProperty);
            Assert.IsTrue(frequencyProperty.CanWrite);
            Assert.IsTrue(frequencyProperty.SetMethod.IsPublic);
        }

        [TestMethod]
        public void Dispose_CanBeCalledWithoutError()
        {
            // Arrange
            var model = new SiteMapItemActionModel();

            // Act & Assert - Should not throw
            model.Dispose();
        }

        [TestMethod]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            // Arrange
            var model = new SiteMapItemActionModel();

            // Act & Assert - Should not throw
            model.Dispose();
            model.Dispose();
            model.Dispose();
        }

        [TestMethod]
        public void Priority_WithVerySmallValue_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var verySmallValue = double.Epsilon;

            // Act
            model.Priority = verySmallValue;

            // Assert
            Assert.AreEqual(verySmallValue, model.Priority);
        }

        [TestMethod]
        public void Priority_WithVeryLargeValue_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var veryLargeValue = double.MaxValue;

            // Act
            model.Priority = veryLargeValue;

            // Assert
            Assert.AreEqual(veryLargeValue, model.Priority);
        }

        [TestMethod]
        public void Action_WithGenericMethod_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapItemActionModel();
            var genericMethod = typeof(TestController).GetMethod("GenericTestAction");

            // Act
            model.Action = genericMethod;

            // Assert
            Assert.AreEqual(genericMethod, model.Action);
            Assert.IsTrue(model.Action.IsGenericMethodDefinition);
        }

        // Test controller class for testing purposes
        private class TestController : Controller
        {
            public IActionResult TestAction()
            {
                return Ok();
            }

            public IActionResult AnotherTestAction()
            {
                return Ok();
            }

            public static IActionResult StaticTestAction()
            {
                return new OkResult();
            }

            private IActionResult PrivateTestAction()
            {
                return Ok();
            }

            public IActionResult GenericTestAction<T>()
            {
                return Ok();
            }
        }
    }
}