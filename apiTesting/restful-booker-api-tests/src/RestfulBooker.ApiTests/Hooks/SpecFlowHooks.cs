using RestfulBooker.ApiTests.Playwright;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Hooks;

[Binding]
public class SpecFlowHooks
{
      private readonly ScenarioContext _scenarioContext;
      private readonly ApiTestBase _apiTestBase;

      public SpecFlowHooks(
          ScenarioContext scenarioContext,
          ApiTestBase apiTestBase)
      {
            _scenarioContext = scenarioContext;
            _apiTestBase = apiTestBase;
      }

      [BeforeScenario]
      public async Task BeforeScenario()
      {
            await _apiTestBase.InitializeApiContextAsync();
            _scenarioContext["ApiContext"] = _apiTestBase.ApiContext;
      }

      [AfterScenario]
      public async Task AfterScenario()
      {
            await _apiTestBase.CleanupApiContextAsync();
      }
}