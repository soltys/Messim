using System.Web;
using System.Web.Routing;
using Messim.UI;
using Moq;
using NUnit.Framework;

namespace Messum.UI.Test.Helpers
{
    public static class RouteHelpers
    {
        public static RouteData TestRoute(string url, object expectedValues)
        {
            // Arrange (obtain routing config + set up test context)
            RouteCollection routeConfig = new RouteCollection();
            MvcApplication.RegisterRoutes(routeConfig);
            var mockHttpContext = MakeMockHttpContext(url);

            // Act (run the routing engine against this HttpContextBase)
            RouteData routeData = routeConfig.GetRouteData(mockHttpContext.Object);
            // Assert
            Assert.IsNotNull(routeData.Route, "No route was matched");

            var expectedDict = new RouteValueDictionary(expectedValues);
            foreach (var expectedVal in expectedDict)
            {
                if (expectedVal.Value == null)
                    Assert.IsNull(routeData.Values[expectedVal.Key]);
                else
                    Assert.AreEqual(expectedVal.Value.ToString(),
                    routeData.Values[expectedVal.Key].ToString());
            }
            return routeData;
        }

        private static Mock<HttpContextBase> MakeMockHttpContext(string url)
        {
            var mockHttpContext = new Mock<HttpContextBase>();
            // Mock the request
            var mockRequest = new Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);
            // Mock the response
            var mockResponse = new Mock<HttpResponseBase>();
            mockHttpContext.Setup(x => x.Response).Returns(mockResponse.Object);
            mockResponse.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(x => x);
            return mockHttpContext;
        }
    }
}