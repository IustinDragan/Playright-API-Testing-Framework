using Microsoft.Playwright;

namespace RestfulBooker.ApiTests.API;

public interface IBookingService
{
      Task<IAPIResponse> CreateBookingAsync(CreateBookingRequestModel requestModel);
      Task<IAPIResponse> UpdateBookingAsync(int id, UpdateBookingRequestModel requestModel, string token);
      Task<IAPIResponse> GetAllBookingAsync();
      Task<IAPIResponse> GetBookingByIdAsync(int id);
      Task<IAPIResponse> DeleteBookingAsync(int id, string token);
}