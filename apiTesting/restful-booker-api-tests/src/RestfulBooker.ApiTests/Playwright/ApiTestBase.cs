using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace RestfulBooker.ApiTests.Playwright;

public class ApiTestBase : PlaywrightTest
{
      public IAPIRequestContext? ApiContext { get; private set; }

      public async Task InitializeApiContextAsync()
      {
            if (ApiContext is not null)
                  return;

            var playwrightContext = await Microsoft.Playwright.Playwright.CreateAsync();

            ApiContext = await playwrightContext.APIRequest.NewContextAsync(
                  new APIRequestNewContextOptions
                  {
                        BaseURL = "https://restful-booker.herokuapp.com",
                        ExtraHTTPHeaders = new Dictionary<string, string>
                        {
                              {"Accept", "appliication/json"}
                        }
                  }
            );
      }

      public async Task CleanupApiContextAsync()
      {
            if (ApiContext is not null)
            {
                  await ApiContext.DisposeAsync();
                  ApiContext = null;
            }
      }
}