using NUnit.Framework;
using System;
using TrafficComet.Abstracts.Logs;
using TrafficComet.Core.Extensions;
using TrafficComet.Core.Tests.Mock;
using TrafficComet.Core.Tests.Tests.Mocks;

namespace TrafficComet.Core.Tests.Tests.Extensions
{
    [TestFixture]
    public class ITrafficLogParams_ValidParams_Tests
    {
        [Test]
        public void CorrectObject()
        {
            var mockObject = ITrafficLogParamsMockFactory.CreateObjectMock(MockStaticData.TraceId, MockStaticData.UserUniqueId,
                MockStaticData.ApplicationId, DateTime.UtcNow, DateTime.UtcNow,
                MockStaticData.RequestBody, MockStaticData.ResponseBody);
            Assert.DoesNotThrow(() => mockObject.ValidParams());
        }

        [Test]
        public void NullObject()
        {
            ITrafficLogParams mock = null;
            Assert.Throws<ArgumentNullException>(() => mock.ValidParams());
        }

        [TestCase(null, "test_client", "test_app")]
        [TestCase("test_trace_id", null, "test_app")]
        [TestCase("test_trace_id", "test_client", null)]
        public void OneOfTheParamsIsWrong(string traceId, string clientUniqueId, string applicationId)
        {
            var mockObject = ITrafficLogParamsMockFactory.CreateObjectMock(traceId, clientUniqueId,
                applicationId, DateTime.UtcNow, DateTime.UtcNow);

            Assert.Throws<NullReferenceException>(() => mockObject.ValidParams());
        }
    }
}