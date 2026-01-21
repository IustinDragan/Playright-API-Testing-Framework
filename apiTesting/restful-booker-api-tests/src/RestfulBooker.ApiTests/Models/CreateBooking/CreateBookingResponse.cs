namespace RestfulBooker.ApiTests.Models;

public class CreateBookingResponse
{
      public int bookingid { get; set; }
      public CreateBookingRequest booking { get; set; } = new();
}