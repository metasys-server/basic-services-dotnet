using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using NUnit.Framework;
using JohnsonControls.Metasys.BasicServices;

namespace MetasysServices.Tests
{
    /// <summary>
    /// Tests for Audits.
    /// </summary>
    public class MetasysClientAuditsTests : MetasysClientTestsBase
    {

        [Test]
        public void TestGetAuditsNone()
        {
            var response = @"{
            ""total"": 0,
            ""next"": null,
            ""previous"": null, 
            ""items"": [],
            ""self"": ""https://win2016-vm2/api/v2/audits?page=1&pageSize=100&excludeDiscarded=false&sort=-creationTime""
            ";
            httpTest.RespondWith(response);
            var audits = client.Audits.GetAudits(new AuditFilter { }); // No filter
            httpTest.ShouldHaveCalled($"https://hostname/api/v2/audits")
                .WithVerb(HttpMethod.Get)
                .Times(1);
            Assert.AreEqual(0, audits.Items.Count());
        }

        [Test]
        public void TestGetAuditsOnePage()
        {
            var response = @"{
            ""total"": 1,
            ""next"": null,
            ""previous"": null,
            ""items"": [" + Audit + @"],
            ""self"": ""https://win2016-vm2/api/v2/audits?page=1&pageSize=100&excludeDiscarded=false&sort=-creationTime""
            ";
            httpTest.RespondWith(response);

            var audits = client.Audits.GetAudits(AuditFilter);
            httpTest.ShouldHaveCalled($"https://hostname/api/v2/audits")
                .WithVerb(HttpMethod.Get)
                .Times(1);
            var expected = JsonConvert.DeserializeObject<AuditItemProvider>(Audit);
            Assert.AreEqual(expected, audits.Items.ElementAt(0));
        }

        [Test]
        public void TestGetAuditsManyPages()
        {
            var response = @"{
            ""total"": 2,
            ""next"": ""https://win2016-vm2/api/v2/audits?page=2&pageSize=100&excludeDiscarded=false&sort=-creationTime"",
            ""previous"": null,
            ""items"": [" + Audit + @"],
            ""self"": ""https://win2016-vm2/api/v2/audits?page=1&pageSize=100&excludeDiscarded=false&sort=-creationTime""
            ";
            httpTest
             .RespondWith(response);
            var audits = client.Audits.GetAudits(AuditFilter);
            httpTest.ShouldHaveCalled($"https://hostname/api/v2/audits")
                .WithVerb(HttpMethod.Get)
                .Times(1);
            var expected = JsonConvert.DeserializeObject<AuditItemProvider>(Audit);
            Assert.AreEqual(expected, audits.Items.ElementAt(0));
        }

        [Test]
        public void TestGetAuditsMissingItems()
        {
            var response = @"{
            ""total"": 0,
            ""next"": null,
            ""previous"": null,
            ""items"": [{}],
            ""self"": ""https://win2016-vm2/api/v2/audits?page=1&pageSize=100&excludeDiscarded=false&sort=-creationTime""
            ";
            httpTest.RespondWith(response);
            Assert.Throws<MetasysObjectException>(() => client.Audits.GetAudits(new AuditFilter { }));
            httpTest.ShouldHaveCalled($"https://hostname/api/v2/audits")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Test]
        public void TestAuditMissingValuesThrowsException()
        {
            string Audit = string.Concat("{",
               "\"id\": \"", mockid, "\",",
               "\"typeUrl\": \"https://hostname/api/v2/enumsets/501/members/7\"}");
            var response = @"{
            ""total"": 1,
            ""next"": null,
            ""previous"": null,
            ""items"": [" + Audit + @"],
            ""self"": ""https://win2016-vm2/api/v2/audits?page=1&pageSize=100&excludeDiscarded=false&sort=-creationTime""
            ";
            httpTest.RespondWith(response);

            var e = Assert.Throws<MetasysObjectException>(() => client.Audits.GetAudits(new AuditFilter { }));

            httpTest.ShouldHaveCalled($"https://hostname/api/v2/audits")
                .WithVerb(HttpMethod.Get)
                .Times(1);
            PrintMessage($"TestGetSpacesMissingValuesThrowsException: {e.Message}", true);
        }

        [Test]
        public void TestGetAuditsUnauthorizedThrowsException()
        {
            httpTest.RespondWith("Unauthorized", 401);

            var e = Assert.Throws<MetasysHttpException>(() => client.Audits.GetAudits(new AuditFilter { }));

            httpTest.ShouldHaveCalled($"https://hostname/api/v2/audits")
                .WithVerb(HttpMethod.Get)
                .Times(1);
            PrintMessage($"TestGetAuditsUnauthorizedThrowsException: {e.Message}", true);
        }
    }
}
