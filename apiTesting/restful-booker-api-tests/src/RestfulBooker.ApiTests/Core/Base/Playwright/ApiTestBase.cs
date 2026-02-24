using Microsoft.Playwright;
using RestfulBooker.ApiTests.Common;

namespace RestfulBooker.ApiTests.Core;

public class ApiTestBase
{
      private readonly string _baseUrl;
      private IPlaywright? _playwright;
      public IAPIRequestContext? ApiContext { get; private set; }

      public ApiTestBase(string baseUrl)
      {
            _baseUrl = baseUrl;
      }

      public async Task InitializeApiContextAsync(int? timeoutInMs = null)
      {
            if (ApiContext is not null)
                  return;

            _playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            ApiContext = await _playwright.APIRequest.NewContextAsync(
                new APIRequestNewContextOptions
                {
                      BaseURL = _baseUrl,
                      Timeout = timeoutInMs,
                      ExtraHTTPHeaders = HeaderFactory.DefaulHeaders()
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

            _playwright?.Dispose();
            _playwright = null;
      }
}