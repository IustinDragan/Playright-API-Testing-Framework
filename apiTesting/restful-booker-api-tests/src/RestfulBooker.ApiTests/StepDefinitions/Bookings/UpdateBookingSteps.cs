using System.Text.Json;
using RestfulBooker.ApiTests.API;
using RestfulBooker.ApiTests.Common;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.StepDefinitions;

[Binding]
public class UpdateBookingSteps
{
      private readonly IBookingService _bookingService;
      private readonly BookingScenarioContext _bookingScenarioContext;

      public UpdateBookingSteps(IBookingService bookingService, BookingScenarioContext bookingScenarioContext)
      {
            _bookingService = bookingService;
            _bookingScenarioContext = bookingScenarioContext;
      }

      [Given(@"I have a valid booking payload for update")]
      public void GivenIHaveAValidBookingPayloadForUpdate()
      {
            _bookingScenarioContext.UpdateBookingRequestModel = BookingFactoryFromJson.UpdateValidBookingFromJson();
      }

      [When(@"I send a PUT request to update the created booking")]
      public async Task WhenISendAPutRequestToUpdateTheCreatedBooking()
      {
            Assert.IsNotNull(_bookingScenarioContext.BookingId, "BookingId is null. Make sure you created a booking first");
            Assert.IsNotNull(_bookingScenarioContext.AuthToken, "AuthToken is null. Make sure you authenticated first");
            Assert.IsNotNull(_bookingScenarioContext.UpdateBookingRequestModel, "UpdateBookingRequest is null");

            _bookingScenarioContext.Response = await _bookingService.UpdateBookingAsync(
                  _bookingScenarioContext.BookingId.Value,
                  _bookingScenarioContext.UpdateBookingRequestModel!,
                  _bookingScenarioContext.AuthToken!
            );
      }
      [When(@"I send a PUT request to update the created booking with id (.*)")]
      public async Task WhenISendAPutRequestToUpdateTheBookingWithId(int id)
      {
            Assert.IsNotNull(_bookingScenarioContext.AuthToken, "AuthToken is null. Make sure you authenticated first");
            Assert.IsNotNull(_bookingScenarioContext.UpdateBookingRequestModel, "UpdateBookingRequest is null");

            _bookingScenarioContext.Response = await _bookingService.UpdateBookingAsync(
                  id, _bookingScenarioContext.UpdateBookingRequestModel!, _bookingScenarioContext.AuthToken!
            );
      }

      [Then(@"booking values should be updated")]
      public async Task ThenBookingValuesShouldBeUpdated()
      {
            Assert.IsNotNull(_bookingScenarioContext.Response, "Response is null");

            var updatedBookingJson = await _bookingScenarioContext.Response!.DeserializeAsync<JsonElement>();

            BookingAssertions.AssertBookingUpdate(updatedBookingJson, _bookingScenarioContext.UpdateBookingRequestModel!);
      }
}