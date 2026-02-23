using TechTalk.SpecFlow;
using RestfulBooker.ApiTests.API;
using RestfulBooker.ApiTests.Common;
using RestfulBooker.ApiTests.Data;

namespace RestfulBooker.ApiTests.StepDefinitions;

[Binding]
public class CommonResponseSteps
{
      private readonly BookingScenarioContext _bookingScenarioContext;
      private readonly SqliteService _db;

      public CommonResponseSteps(BookingScenarioContext bookingScenarioContext, SqliteService db)
      {
            _bookingScenarioContext = bookingScenarioContext;
            _db = db;
      }

      [Then(@"the response status code should be (.*)")]
      public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
      {
            BookingAssertions.ValidateStatus(expectedStatusCode, _bookingScenarioContext.Response.Status);
      }

      [Then(@"the response should be in JSON format")]
      public async Task ThenTheResponseShouldBeInJsonFormat()
      {
            _bookingScenarioContext.Response.Headers.TryGetValue(Constants.ContentType, out var contentType);

            var body = await _bookingScenarioContext.Response.TextAsync();

            JsonDeserializerHelper.ValidateJsonFormat(contentType!, body);
      }

      [Then(@"the booking id should be returned")]
      public async Task ThenTheBookingIdShouldBeReturned()
      {
            var response = await _bookingScenarioContext.Response.DeserializeAsync<CreateBookingResponseModel>();

            BookingAssertions.ValidateBookingId(response);

            _bookingScenarioContext.BookingId = response.BookingId;
      }

      [Then(@"I save the response to database")]
      public async Task ThenISaveTheResponseToDatabase()
      {
            Assert.IsNotNull(_bookingScenarioContext.Response, "Response is null");
            Assert.IsNotNull(_bookingScenarioContext.BookingId, "BookingId is null.");

            if (_bookingScenarioContext.Response.Status != 200)
                  Assert.Inconclusive("Skip DB insert because response status is not 200.");

            var body = await _bookingScenarioContext.Response.TextAsync();

            SchemaValidator.Validate("Data/TestData/CreateBookingSchema.json", body);

            _db.Insert(body);
            //_db.Insert(_bookingScenarioContext.BookingId!.Value, body);
      }

      //     [Then(@"I update the response in database")]
      //     public async Task ThenIUpdateTheResponseInDatabase()
      //     {
      //         Assert.IsNotNull(_bookingScenarioContext.Response, "Response is null.");
      //         Assert.IsNotNull(_bookingScenarioContext.BookingId, "BookingId is null.");

      //         if (_bookingScenarioContext.Response.Status != 200)
      //             Assert.Inconclusive("Skip DB update because response status is not 200.");

      //         var body = await _bookingScenarioContext.Response.TextAsync();

      //         _db.UpdateByBookingId(
      //             _bookingScenarioContext.BookingId.Value,
      //             body
      //         );
      // }

}