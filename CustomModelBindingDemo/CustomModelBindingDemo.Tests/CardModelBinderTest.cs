using CustomModelBindingDemo.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Web;
using System.Collections.Specialized;
using CustomModelBindingDemo.Models;
using Moq;

namespace CustomModelBindingDemo.Tests
{
    [TestClass]
    public class CardModelBinderTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void CanBindToExpiry()
        {
            // Arrange
            var formCollection = new NameValueCollection { 
                { "foo.CardNo", "9999999999999999" },
                { "foo.Code", "999" }
            };

            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Card));

            var bindingContext = new ModelBindingContext
            {
                ModelName = "foo",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext
                .SetupGet(c => c.Request["Expiry.Value.Month"])
                .Returns(() => "7");
            mockHttpContext
                .SetupGet(c => c.Request["Expiry.Value.Year"])
                .Returns(() => "2014");

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };

            CardModelBinder b = new CardModelBinder();

            // Act
            Card result = (Card)b.BindModel(controllerContext, bindingContext);

            // Assert
            Assert.AreEqual(7, result.Expiry.Value.Month, "Incorrect value for Month");
            Assert.AreEqual(2014, result.Expiry.Value.Year, "Incorrect value for Year");
        }
    }
}
