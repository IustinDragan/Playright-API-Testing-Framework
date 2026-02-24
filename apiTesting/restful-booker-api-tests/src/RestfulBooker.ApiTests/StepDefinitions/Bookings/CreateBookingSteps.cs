using RestfulBooker.ApiTests.API;
using RestfulBooker.ApiTests.API.Builders;
using RestfulBooker.ApiTests.Common;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.StepDefinitions;

[Binding]
public class CreateBookingSteps
{
      private readonly IBookingService _bookingService;
      private readonly BookingScenarioContext _bookingScenarioContext;

      public CreateBookingSteps(IBookingService bookingService, BookingScenarioContext bookingScenarioContext)
      {
            _bookingService = bookingService;
            _bookingScenarioContext = bookingScenarioContext;
      }

      [Given(@"I prepare a valid booking payload")]
      public void GivenIHaveAValidBookingPayload()
      {
            #region Create Booking from JsonFile
            //_bookingScenarioContext.CreateBookingRequest = BookingFactoryFromJson.ValidBookingFromJson();
            #endregion

            #region Create Booking from default Builder
            _bookingScenarioContext.CreateBookingRequest = BookingFactoryFromBuilder.ValidBooking().Build();
            #endregion

            #region Create Booking from customer Builder
            //_bookingScenarioContext.CreateBookingRequest = BookingFactoryFromBuilder.ValidBooking().WithFirstName("from_custom_builder").Build<CreateBookingRequestModel>();
            #endregion
      }
      [Given(@"I prepare an invalid booking payload")]
      public void GivenIPrepareAnInvalidBookingPayload()
      {
            _bookingScenarioContext.CreateBookingRequest = BookingFactoryFromBuilder.InvalidBooking().Build();
      }

      [When(@"I send a POST request to create a booking")]
      public async Task WhenISendAPostRequest()
      {
            _bookingScenarioContext.Response = await _bookingService.CreateBookingAsync(_bookingScenarioContext.CreateBookingRequest!);
      }
}