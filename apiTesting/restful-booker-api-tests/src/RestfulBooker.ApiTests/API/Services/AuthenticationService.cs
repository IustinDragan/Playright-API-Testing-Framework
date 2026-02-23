using System.Reflection.Metadata;
using System.Text.Json;
using Microsoft.Playwright;
using RestfulBooker.ApiTests.Common;
using RestfulBooker.ApiTests.Core;

namespace RestfulBooker.ApiTests.API;

public class AuthenticationService : IAuthenticationService
{
      private readonly ApiTestBase _apiTestBase;

      private IAPIRequestContext _apiContext => _apiTestBase.ApiContext ?? throw new InvalidOperationException("ApiContext not initialized");


      public AuthenticationService(ApiTestBase apiTestBase)
      {
            _apiTestBase = apiTestBase;
      }


      public async Task<IAPIResponse> AuthenticateAsync(AuthRequestModel authRequestModel)
      {
            var authResponse = await _apiContext.PostAsync(
                  Constants.AuthEndpoint,
                  new APIRequestContextOptions
                  {
                        Data = JsonSerializer.Serialize(new
                        {
                              username = authRequestModel.Username,
                              password = authRequestModel.Password
                        }, JsonSerializerHelper.Options)
                  }
            );

            Assert.AreEqual(200, authResponse.Status, "Auth endpoint did not return 200");

            return authResponse;
      }
}