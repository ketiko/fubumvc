using FubuMVC.IntegrationTesting.Conneg;
using NUnit.Framework;
using TestPackage1;
using FubuTestingSupport;
using StringController = TestPackage1.StringController;

namespace FubuMVC.IntegrationTesting.Packaging
{
    [TestFixture]
    public class invoking_endpoints_from_a_package : FubuRegistryHarness
    {
        protected override void initializeBottles()
        {
            runBottles(@"
init src/TestPackage1 pak1
link harness pak1
");
        }

        [Test]
        public void can_invoke_a_json_endpoint_in_a_package()
        {
            // This endpoint is in the TestPackage1 package
            endpoints.PostJson(new JsonSerializedMessage{
                Name = "Jeremy"
            })
                .ReadAsJson<JsonSerializedMessage>()
                .Name.ShouldEqual("Jeremy");
        }

        [Test]
        public void can_invoke_string_endpoint_from_a_package()
        {
            endpoints.Get<StringController>(x => x.SayHello())
                .ReadAsText()
                .ShouldEqual("Hello");
        }
    }
}