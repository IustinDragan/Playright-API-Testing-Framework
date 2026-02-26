using RestfulBooker.ApiTests.API;
using RestfulBooker.ApiTests.Common;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.StepDefinitions;

[Binding]
public class HealthCheckSteps
{
      private BookingScenarioContext _bookingScenarioContext;
      private IBookingService _bookingService;

      public HealthCheckSteps(BookingScenarioContext bookingScenarioContext, IBookingService bookingService)
      {
            _bookingScenarioContext = bookingScenarioContext;
            _bookingService = bookingService;
      }

      [When(@"I send a GET request to healthCheck endpoint")]
      public async Task WhenISendAGETRequestToUpdateHealthCheckEnpoint()
      {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            _bookingScenarioContext.Response = await _bookingService.GetHealthCheck();

            watch.Stop();

            _bookingScenarioContext.ResponseTimeInMs = watch.ElapsedMilliseconds;
      }

      [When(@"I send a POST request to healthCheck endpoint")]
      public async Task WhenISendAPOSTRequestToHealthCheckEndpoint()
      {
            _bookingScenarioContext.Response = await _bookingService.PostHealthCheck();
      }

      [Then(@"the response header ""(.*)"" should contain ""(.*)""")]
      public async Task ThenTheResponseHeaderShouldContain(string headerName, string expectedValue)
      {
            Assert.IsNotNull(_bookingScenarioContext.Response, "Response is null. The request step likely did not run or fauled.");

            var headers = _bookingScenarioContext.Response.Headers;
            var header = headers.FirstOrDefault(x => string.Equals(x.Key, headerName, StringComparison.OrdinalIgnoreCase));

            Assert.IsTrue(header.Value.Contains(expectedValue, StringComparison.OrdinalIgnoreCase),
                  $"Expected header {headerName} to contain {expectedValue} but was {header.Value}");
      }

      [Then(@"the response time should be less than (.*) ms")]
      public async Task ThenTheResponseTimeShouldBeLessThan(int maxMilliseconts)
      {
            var responseTime = _bookingScenarioContext.ResponseTimeInMs;

            Assert.IsTrue(responseTime!.Value <= responseTime, $"Expected response time to be less than {maxMilliseconts} ms but was {responseTime.Value} ms.");
      }
}