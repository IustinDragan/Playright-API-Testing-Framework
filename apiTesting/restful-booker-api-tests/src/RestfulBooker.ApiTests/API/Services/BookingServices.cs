using System.Text.Json;
using Microsoft.Playwright;
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
                      Headers = HeaderFactory.AuthHeaders(token)
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
                      Headers = HeaderFactory.AuthHeaders(token)
                }
            );
      }

      public async Task<IAPIResponse> UpdateBookingSwitchAsync(BookingUpdateMethod method, int id, UpdateBookingRequestModel requestModel, string token)
      {
            return method switch
            {
                  BookingUpdateMethod.Put => await SendUpdateAsync(id, requestModel, token, BookingUpdateMethod.Put),
                  BookingUpdateMethod.Patch => await SendUpdateAsync(id, requestModel, token, BookingUpdateMethod.Patch),
                  _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
            };
      }

      public async Task<IAPIResponse> GetHealthCheck()
      {
            return await _apiContext.GetAsync(Constants.HealthCheck);
      }

      public async Task<IAPIResponse> PostHealthCheck()
      {
            return await _apiContext.PostAsync(Constants.HealthCheck);
      }

      private async Task<IAPIResponse> SendUpdateAsync(int id, UpdateBookingRequestModel requestModel, string token, BookingUpdateMethod verb)
      {
            var options = new APIRequestContextOptions
            {
                  Data = JsonSerializer.Serialize(requestModel, JsonSerializerHelper.Options),
                  Headers = HeaderFactory.AuthHeaders(token)
            };

            var url = Constants.BookingById(id);

            return verb switch
            {
                  BookingUpdateMethod.Put => await _apiContext.PutAsync(url, options),
                  BookingUpdateMethod.Patch => await _apiContext.PatchAsync(Constants.BookingById(id), options),
                  _ => throw new ArgumentOutOfRangeException(nameof(verb), verb, null)
            };
      }
}