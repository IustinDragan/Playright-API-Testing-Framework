using Microsoft.Playwright;
using RestfulBooker.ApiTests.Models;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.StepDefinitions;

[Binding]
[TestCategory("api")]
[TestCategory("booking")]
public class BookingSteps
{
      private readonly ScenarioContext _scenarioContext;
      private CreateBookingRequest _createRequest = null!;
      private UpdateBookingRequest _updateRequest = null!;
      private IAPIResponse _response = null!;
      private IAPIRequestContext _apiContext => (IAPIRequestContext)_scenarioContext["ApiContext"];

      public BookingSteps(ScenarioContext scenarioContext)
      {
            _scenarioContext = scenarioContext;
      }

      [Given(@"I have a valid booking payload")]
      public void GivenIHaveAValidBookingPayload()
      {
            _createRequest = new CreateBookingRequest
            {
                  firstname = "John",
                  lastname = "Doe",
                  totalprice = 150,
                  depositpaid = true,
                  bookingdates = new BookingDates
                  {
                        checkin = "2024-01-01",
                        checkout = "2024-01-05"
                  },
                  additionalneeds = "Breakfast"
            };
      }

      [Given("I have a valid booking payload for update")]
      public async Task GivenIHaveAValidBookingPayloadForUpdate()
      {
            _updateRequest = new UpdateBookingRequest
            {
                  firstname = "Dragan_Updated",
                  lastname = "Iustin_Updated",
                  totalprice = 200,
                  depositpaid = false,
                  bookingdates = new BookingDates
                  {
                        checkin = "2026-02-02",
                        checkout = "2026-02-06"
                  },
                  additionalneeds = "Breakfast"
            };
      }

      [Given(@"I am authenticated")]
      public async Task GivenIAmAuthenticated()
      {
            var authResponse = await _apiContext.PostAsync(
                  "/auth",
                  new APIRequestContextOptions
                  {
                        DataObject = new { username = "admin", password = "password123" },
                        Headers = new Dictionary<string, string>
                        {
                              {"Content-Type", "application/json"}
                        }
                  }
            );

            Assert.AreEqual(200, authResponse.Status, "Authentication endpoint did not return 200");

            var auth = await authResponse.JsonAsync<AuthResponse>();
            Assert.IsFalse(string.IsNullOrWhiteSpace(auth?.token), "Auth token was not returned");

            _scenarioContext["AuthToken"] = auth.token;
      }

      [When(@"I send a POST request to create a booking")]
      public async Task WhenISendAPOSTRequest()
      {
            _response = await _apiContext.PostAsync(
                "/booking",
                new APIRequestContextOptions
                {
                      DataObject = _createRequest,
                      Headers = new Dictionary<string, string>
                      {
                            { "Content-Type", "application/json" },
                            { "Accept", "application/json" }
                      }
                }
            );
      }

      [When(@" I send a PUT request to update the booking with id (.*)")]
      public async Task WhenISendAPutRequestToUpdateTheCreatedBooking(int id)
      {
            var token = (string)_scenarioContext["AuthToken"];

            _response = await _apiContext.PutAsync(
                  $"/booking/{id}",
                  new APIRequestContextOptions
                  {
                        DataObject = _updateRequest,
                        Headers = new Dictionary<string, string>
                        {
                              {"Content-Type", "application/json"},
                              {"Cookie", $"token ={token}"}
                        }
                  }
            );
      }

      [When(@"I request all booking IDs")]
      public async Task WhenIRequestAllBookingIds()
      {
            _response = await _apiContext.GetAsync("/booking");
      }

      [When(@"I send a PUT request to update the created booking")]
      public async Task ISendAPutRequestToUpdateTheCreatedBooking()
      {
            var token = (string)_scenarioContext["AuthToken"];
            int id = (int)_scenarioContext["BookingId"];

            _response = await _apiContext.PutAsync(
                  $"/booking/{id}",
                  new APIRequestContextOptions
                  {
                        DataObject = _updateRequest,
                        Headers = new Dictionary<string, string>
                        {
                              {"Content-Type", "application/json"},
                              {"Accept", "application/json"},
                              {"Cookie", $"token={token}"}
                        }
                  }
            );

      }

      [When(@"I request a specific booking with id (.*)")]
      public async Task WhenIRequestASpecificBookingWithId(int id)
      {
            _response = await _apiContext.GetAsync(
                  $"/booking/{id}",
                  new APIRequestContextOptions
                  {
                        Headers = new Dictionary<string, string>
                        {
                              {"Content-Type", "application/json"},
                              {"Accept", "application/json"}
                        }
                  }
            );
      }

      [When(@"I send a DELETE request to delete the created booking")]
      public async Task WhenISendADeleteRequestToDeleteTheCreatedBooking()
      {
            var token = (string)_scenarioContext["AuthToken"];
            var id = (int)_scenarioContext["BookingId"];

            _response = await _apiContext.DeleteAsync(
                  $"/booking/{id}",
                  new APIRequestContextOptions
                  {
                        Headers = new Dictionary<string, string>
                        {
                              {"Cookie", $"token={token}"}
                        }
                  }
            );
      }

      [Then(@"the response status code should be (.*)")]
      public void ThenTheResponseStatusCodeShouldBe(int statusCode)
      {
            Assert.AreEqual(statusCode, _response.Status);
      }

      [Then(@"the booking id should be returned")]
      public async Task ThenTheBookingIdShouldBeReturned()
      {
            var response = await _response.JsonAsync<CreateBookingResponse>();
            Assert.IsTrue(response?.bookingid > 0);

            _scenarioContext["BookingId"] = response!.bookingid;
      }

      [Then("the response should be in JSON format")]
      public async Task ThenTheResponseShouldBeInJsonFormat()
      {
            _response.Headers.TryGetValue("content-type", out var contentType);
            Assert.IsTrue(contentType?.Contains("application/json") == true);

            var body = await _response.TextAsync();
            System.Text.Json.JsonDocument.Parse(body);
      }

      [Then(@"booking values should be updated")]
      public async Task ThenBookingValuesShouldBeUpdated()
      {
            var response = await _response.JsonAsync<UpdateBookingResponse>();

            Assert.AreEqual(_updateRequest.firstname, response?.firstname);
            Assert.AreEqual(_updateRequest.lastname, response?.lastname);
            Assert.AreEqual(_updateRequest.totalprice, response?.totalprice);
            Assert.AreEqual(_updateRequest.depositpaid, response?.depositpaid);
            Assert.AreEqual(_updateRequest.bookingdates.checkin, response?.bookingdates.checkin);
            Assert.AreEqual(_updateRequest.bookingdates.checkout, response?.bookingdates.checkout);
            Assert.AreEqual(_updateRequest.additionalneeds, response?.additionalneeds);
      }
}
