using Microsoft.Playwright;
using RestfulBooker.ApiTests.API;

namespace RestfulBooker.ApiTests.Common;

public class BookingScenarioContext
{
      public CreateBookingRequestModel CreateBookingRequest { get; set; } = null!;
      public UpdateBookingRequestModel? UpdateBookingRequestModel { get; set; } = null!;
      public IAPIResponse Response { get; set; } = null!;
      public int? BookingId { get; set; }
      public string? AuthToken { get; set; }
      public bool isValidPayload { get; set; } = true;
      public string? ResponseBody { get; set; }
      public bool? apiReady { get; set; }
      public long? ResponseTimeInMs { get; set; }
}