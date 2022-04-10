using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test
{
    public class LoginTest : BaseIntegration
    {
        [Fact]
        public async Task TokenTest()
        {
            var tokenObj = await GetToken("cody_zieme@goldner.co.uk");

            Assert.NotNull(tokenObj);
            Assert.True(tokenObj.authenticated);
            Assert.NotEmpty(tokenObj.accessToken);
        }

    }
}
