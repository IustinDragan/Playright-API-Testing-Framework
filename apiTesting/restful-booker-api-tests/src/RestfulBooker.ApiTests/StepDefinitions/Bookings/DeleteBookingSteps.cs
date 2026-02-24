using System.Security.Cryptography.X509Certificates;
using RestfulBooker.ApiTests.API;
using RestfulBooker.ApiTests.API.Builders;
using RestfulBooker.ApiTests.Common;
using SQLitePCL;
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

      [Given(@"I have created a booking to delete")]
      public async Task GivenIHaveCreatedABookingToDelete()
      {
            _bookingScenarioContext.CreateBookingRequest = BookingFactoryFromBuilder.ValidBooking().Build();
            _bookingScenarioContext.Response = await _bookingService.CreateBookingAsync(_bookingScenarioContext.CreateBookingRequest);

            var response = await _bookingScenarioContext.Response.DeserializeAsync<CreateBookingResponseModel>();
            BookingAssertions.ValidateBookingId(response);

            _bookingScenarioContext.BookingId = response.BookingId;
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

      [Then(@"the booking should be deleted from the system")]
      public async Task ThenTheBookingShouldBeDeletedFromTheSystem()
      {
            try
            {
                  Assert.IsNotNull(_bookingScenarioContext.BookingId, "BookingId is null. Create booking first.");
                  Assert.IsNotNull(_bookingScenarioContext.AuthToken, "AuthToken is null. Authenticate first.");

                  _bookingScenarioContext.Response = await _bookingService.GetBookingByIdAsync(_bookingScenarioContext.BookingId.Value);

                  Assert.AreEqual(404, _bookingScenarioContext.Response.Status, "Expected booking to be deleted (404 Not Found), but it still exists");

            }
            catch (Exception ex)
            {
                  Assert.Fail($"Error verifying booking deletion: {ex.Message}");
            }
      }
}