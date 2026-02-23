using System.Security.Cryptography.X509Certificates;
using RestfulBooker.ApiTests.API;
using RestfulBooker.ApiTests.Common;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.StepDefinitions;

[Binding]
public class DeleteBookingSteps
{
      private readonly IBookingService _bookingService;
      private readonly BookingScenarioContext _bookingScenarioContext;

      public DeleteBookingSteps(IBookingService bookingService, BookingScenarioContext bookingScenarioContext)
      {
            _bookingService = bookingService;
            _bookingScenarioContext = bookingScenarioContext;
      }

      [When(@"I send a DELETE request to delete the created booking")]
      public async Task WhenISendADeleteRequestToDeleteTheCreatedBooking()
      {
            Assert.IsNotNull(_bookingScenarioContext.BookingId, "BookingId is null. Create booking first.");
            Assert.IsNotNull(_bookingScenarioContext.AuthToken, "AuthToken is null. Authenticate first.");

            _bookingScenarioContext.Response = await _bookingService.DeleteBookingAsync(_bookingScenarioContext.BookingId.Value, _bookingScenarioContext.AuthToken!);
      }


      [When(@"I send a DELETE request to delete the booking with id (.*)")]
      public async Task WhenISendADeleteRequestToDeleteTheBookingWithId(int id)
      {
            Assert.IsNotNull(_bookingScenarioContext.AuthToken, "AuthToken is null. Authenticate first.");

            _bookingScenarioContext.Response = await _bookingService.DeleteBookingAsync(id, _bookingScenarioContext.AuthToken!);
      }

      [When(@"I send a DELETE request without being authenticated to delete the created booking")]
      public async Task WhenISendADeleteRequestWithoutBeingAuthenticatedToDeleteTheCreatedBooking()
      {
            Assert.IsNotNull(_bookingScenarioContext.BookingId, "BookingId is null. Create booking first()");

            _bookingScenarioContext.Response = await _bookingService.DeleteBookingAsync(_bookingScenarioContext.BookingId.Value, "");
      }
}