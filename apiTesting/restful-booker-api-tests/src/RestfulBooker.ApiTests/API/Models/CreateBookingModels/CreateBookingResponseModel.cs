namespace RestfulBooker.ApiTests.API;

public class CreateBookingResponseModel
{
      public int BookingId { get; set; }
      public CreateBookingRequestModel Booking { get; set; } = new();
}