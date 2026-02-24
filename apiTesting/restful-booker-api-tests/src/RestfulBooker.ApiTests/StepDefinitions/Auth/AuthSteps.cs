using RestfulBooker.ApiTests.API;
using RestfulBooker.ApiTests.Common;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.StepDefinitions;

[Binding]
public class AuthSteps
{
      private readonly IAuthenticationService _authService;
      private readonly IAuthCredentialsProvider _authCredentialsProvider;
      private readonly BookingScenarioContext _bookingScenarioContext;

      public AuthSteps(IAuthCredentialsProvider authCredentialsProvider, IAuthenticationService authService, BookingScenarioContext bookingScenarioContext)
      {
            _authService = authService;
            _authCredentialsProvider = authCredentialsProvider;
            _bookingScenarioContext = bookingScenarioContext;
      }

      [Given(@"I have a valid authentication token")]
      public async Task GivenIAmAuthenticated()
      {
            var authResponse = await _authService.AuthenticateAsync(_authCredentialsProvider.GetAdminCredentials());

            var auth = await authResponse.DeserializeAsync<AuthResponseModel>();
            var token = auth?.Token;

            _bookingScenarioContext.AuthToken = token;
      }

      [Given(@"I have an invalid authorization token")]
      public void GivenIHaveAnInvalidAuthorizationToken()
      {
            _bookingScenarioContext.AuthToken = Constants.InvalidAuthorizationToken;
      }
}