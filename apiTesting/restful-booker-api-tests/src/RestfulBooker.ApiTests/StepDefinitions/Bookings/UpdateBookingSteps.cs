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

      [Given(@"I prepare a valid booking payload for (.*)")]
      public void GivenIHaveAValidBookingPayloadForUpdate(string updateType)
      {
            _bookingScenarioContext.UpdateBookingRequestModel = SelectUpdateType(updateType);
      }

      [When(@"I send a (.*) request to update the created booking")]
      public async Task WhenISendAPutRequestToUpdateTheCreatedBooking(string actionType)
      {
            Assert.IsNotNull(_bookingScenarioContext.BookingId, "BookingId is null. Make sure you created a booking first");
            Assert.IsNotNull(_bookingScenarioContext.AuthToken, "AuthToken is null. Make sure you authenticated first");
            Assert.IsNotNull(_bookingScenarioContext.UpdateBookingRequestModel, "UpdateBookingRequest is null");

            var updateMethod = SelectActionTypeMethod(actionType);

            _bookingScenarioContext.Response = await _bookingService.UpdateBookingSwitchAsync(
                  updateMethod,
                  _bookingScenarioContext.BookingId.Value,
                  _bookingScenarioContext.UpdateBookingRequestModel!,
                  _bookingScenarioContext.AuthToken!
            );
      }
      [When(@"I send a (.*) request to (.*) the booking with id (.*)")]
      public async Task WhenISendAPutRequestToUpdateTheBookingWithId(string action, string actionType, int id)
      {
            Assert.IsNotNull(_bookingScenarioContext.AuthToken, "AuthToken is null. Make sure you authenticated first");
            Assert.IsNotNull(_bookingScenarioContext.UpdateBookingRequestModel, "UpdateBookingRequest is null");

            _bookingScenarioContext.Response = await _bookingService.UpdateBookingAsync(
                  id, _bookingScenarioContext.UpdateBookingRequestModel!, _bookingScenarioContext.AuthToken!
            );
      }

      [When(@"I send a (.*) request with (.*) authorization to update the created booking")]
      public async Task WhenISendARequestWithAuthorizationToUpdateTheCreatedBooking(string actionType, string token)
      {
            Assert.IsNotNull(_bookingScenarioContext.AuthToken, "AuthToken is null. Make sure you authenticated first");
            Assert.IsNotNull(_bookingScenarioContext.UpdateBookingRequestModel, "UpdateBookingRequest is null");

            var updateMethod = SelectActionTypeMethod(actionType);
            var invalidToken = SelectInvalidAuthToken(token);

            _bookingScenarioContext.Response = await _bookingService.UpdateBookingSwitchAsync(
                  updateMethod,
                  _bookingScenarioContext.BookingId!.Value,
                  _bookingScenarioContext.UpdateBookingRequestModel!,
                  invalidToken);
      }

      [Then(@"booking values should be updated")]
      public async Task ThenBookingValuesShouldBeUpdated()
      {
            Assert.IsNotNull(_bookingScenarioContext.Response, "Response is null");

            var updatedBookingJson = await _bookingScenarioContext.Response!.DeserializeAsync<JsonElement>();

            BookingAssertions.AssertBookingUpdate(updatedBookingJson, _bookingScenarioContext.UpdateBookingRequestModel!);
      }

      public BookingUpdateMethod SelectActionTypeMethod(string actionType)
      {
            return actionType.Equals(BookingUpdateMethod.Put.ToString(), StringComparison.OrdinalIgnoreCase)
                  ? BookingUpdateMethod.Put
                  : BookingUpdateMethod.Patch;
      }

      public string SelectInvalidAuthToken(string token)
      {
            return token.ToLowerInvariant() switch
            {
                  "missing" => string.Empty,
                  "invalid" => Constants.InvalidAuthorizationToken,
                  _ => throw new ArgumentOutOfRangeException(nameof(token), token, "Only missing or invalid authentication token are supported.")
            };
      }

      private UpdateBookingRequestModel SelectUpdateType(string updateType)
      {
            return updateType switch
            {
                  "update" => _bookingScenarioContext.UpdateBookingRequestModel = BookingFactoryFromJson.UpdateValidBookingFromJson(),
                  "partial update" => _bookingScenarioContext.UpdateBookingRequestModel = BookingFactoryFromJson.UpdateBookingWithFirstName("Test partial update"),
                  _ => throw new ArgumentOutOfRangeException(nameof(updateType), updateType)
            };
      }
}