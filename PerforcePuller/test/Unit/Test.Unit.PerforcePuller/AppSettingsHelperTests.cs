using System;
using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;
using PerforcePuller.Helpers;

namespace Test.Unit.PerforcePuller
{
    [TestFixture]
    internal class AppSettingsHelperTests
    {


        [OneTimeSetUp]
        public void SetUpFakeConfig()
        {
            ConfigurationManager.AppSettings.Set("IntKey", "15");
            ConfigurationManager.AppSettings.Set("IntKeyException", "Nan");
            ConfigurationManager.AppSettings.Set("EmptyKeyException", "");
        }

        [Test]
        public void AppSettingsHelper_Get_ReturnsExpectedType()
        {
            var result = AppSettingsHelper.Get<int>("IntKey");
            Assert.That(result, Is.TypeOf(typeof(int)));
        }

        [Test]
        public void AppSettingsHelper_Get_InvalidCastException()
        {

            Assert.Throws<InvalidCastException>(delegate { AppSettingsHelper.Get<int>("IntKeyException"); });
        }

        private static IEnumerable<TestCaseData> MissingOrEmptyAppSettings()
        {
            yield return new TestCaseData("EmptyKeyException");
            yield return new TestCaseData("NonExistantKeyException");
        }

        [Test, TestCaseSource("MissingOrEmptyAppSettings")]
        public void AppSettingsHelper_Get_SettingsPropertyNotFoundException(string appKey)
        {
            Assert.Throws<SettingsPropertyNotFoundException>(delegate { AppSettingsHelper.Get<string>(appKey); });
        }

        [OneTimeTearDown]
        public void RemoveFakeConfig()
        {
            ConfigurationManager.AppSettings.Remove("IntKey");
            ConfigurationManager.AppSettings.Remove("IntKeyException");
            ConfigurationManager.AppSettings.Remove("EmptyKeyException");
        }
    }
}
