using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;
using RestfulBooker.ApiTests.Core;
using RestfulBooker.ApiTests.Data;
using RestfulBooker.ApiTests.Helpers;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Hooks;

[Binding]
public class SpecFlowHooks
{
      private const int _defaultTimeoutMs = 30_000;
      private const int _slowTimeoutMs = 60_000;
      private readonly ScenarioContext _scenarioContext;
      private readonly ApiTestBase _apiTestBase;
      private readonly SqliteService _db;

      public SpecFlowHooks(ApiTestBase apiTestBase, ScenarioContext scenarioContext, SqliteService db)
      {
            _scenarioContext = scenarioContext;
            _apiTestBase = apiTestBase;
            _db = db;
      }

      [BeforeScenario]
      public async Task BeforeScenario()
      {
            var timeout = _scenarioContext.ScenarioInfo.Tags.Contains("slow") ? _slowTimeoutMs : _defaultTimeoutMs;

            if (_scenarioContext.ScenarioInfo.Tags.Any(x => string.Equals(x, "skip", StringComparison.OrdinalIgnoreCase)))
                  Assert.Inconclusive("Scenario skipped via @skip tag");

            await _apiTestBase.InitializeApiContextAsync(timeout);

            _db.Initialize();
      }

      [AfterScenario]
      public async Task AfterScenario()
      {
            await _apiTestBase.CleanupApiContextAsync();

            SpecFlowReportGenerator.RecordScenarioResult(_scenarioContext);
      }

      [AfterTestRun]
      public static async Task AfterTestRun()
      {
            //SpecFlow Report:
            await SpecFlowReportGenerator.GenerateReportAsync();

            //Playwright Report:
            await PlaywrightReportHelper.OrganizeReportAsync();
      }
}