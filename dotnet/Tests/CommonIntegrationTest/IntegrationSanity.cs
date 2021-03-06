﻿using CommonIntegrationTest._Utilities;
using ExampleModel.Wire;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Shouldly;
using System.Threading.Tasks;

namespace CommonIntegrationTest
{
    [TestClass]
    public class IntegrationSanity
    {

        [TestMethod]
        public async Task TestIntegrationTests()
        {
            var response = await TestSetup.Client.GetAsync("api/example/status");

            string responseBody = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Response>(responseBody);
            json.Success.ShouldBeTrue(responseBody);
        }
    }
}
