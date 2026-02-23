using System.Text.Json;
using Microsoft.Playwright;
using RestfulBooker.ApiTests.API;
using RestfulBooker.ApiTests.Common;
using RestfulBooker.ApiTests.Core;

namespace RestfulBooker.ApiTests.API;

public class BookingService : IBookingService
{
      private readonly ApiTestBase _apiTestBase;
      private IAPIRequestContext _apiContext => _apiTestBase.ApiContext ?? throw new InvalidOperationException("ApiContext not initialized");

      public BookingService(ApiTestBase apiTestBase)
      {
            _apiTestBase = apiTestBase;
      }

      public async Task<IAPIResponse> CreateBookingAsync(CreateBookingRequestModel requestModel)
      {
            return await _apiContext.PostAsync(
                Constants.BookingEndpoint,
                new APIRequestContextOptions
                {
                      Data = JsonSerializer.Serialize(requestModel, JsonSerializerHelper.Options)
                }
            );
      }

      public async Task<IAPIResponse> UpdateBookingAsync(int id, UpdateBookingRequestModel requestModel, string token)
      {
            return await _apiContext.PutAsync(
                Constants.BookingById(id),
                new APIRequestContextOptions
                {
                      Data = JsonSerializer.Serialize(requestModel, JsonSerializerHelper.Options),
                      Headers = new Dictionary<string, string>
                    {
                    {Constants.Cookie, Constants.TokenValue(token)}
                    }
                }
            );
      }

      public async Task<IAPIResponse> GetAllBookingAsync()
      {
            return await _apiContext.GetAsync(Constants.BookingEndpoint);
      }

      public async Task<IAPIResponse> GetBookingByIdAsync(int id)
      {
            return await _apiContext.GetAsync(Constants.BookingById(id));
      }

      public async Task<IAPIResponse> DeleteBookingAsync(int id, string token)
      {
            return await _apiContext.DeleteAsync(
                Constants.BookingById(id),
                new APIRequestContextOptions
                {
                      Headers = new Dictionary<string, string>
                    {
                   {Constants.Cookie, Constants.TokenValue(token)}
                    }
                }
            );
      }
}