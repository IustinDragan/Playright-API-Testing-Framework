using Microsoft.Playwright;

namespace RestfulBooker.ApiTests.API;

public interface IAuthenticationService
{
      Task<IAPIResponse> AuthenticateAsync(AuthRequestModel authRequestModel);
}