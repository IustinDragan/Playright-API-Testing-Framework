using RestfulBooker.ApiTests.API;
using RestfulBooker.ApiTests.Common;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.StepDefinitions;

[Binding]
public class GetBookingSteps
{
      private readonly IBookingService _bookingService;
      private readonly BookingScenarioContext _bookingScenarioContext;

      public GetBookingSteps(IBookingService bookingService, BookingScenarioContext bookingScenarioContext)
      {
            _bookingService = bookingService;
            _bookingScenarioContext = bookingScenarioContext;
      }

      [When(@"I request all booking IDs")]
      public async Task WhenIRequestAllBookingIds()
      {
            _bookingScenarioContext.Response = await _bookingService.GetAllBookingAsync();
      }

      [When(@"I request a specific booking with id (.*)")]
      public async Task WhenIRequestASpecificBookingWithId(int id)
      {
            _bookingScenarioContext.Response = await _bookingService.GetBookingByIdAsync(id);
      }
}