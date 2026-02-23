using RestfulBooker.ApiTests.Common;

namespace RestfulBooker.ApiTests.API;

public static class BookingFactoryFromJson
{
      public static CreateBookingRequestModel ValidBookingFromJson()
      {
            return JsonTestDataReader.LoadJsonFile<CreateBookingRequestModel>("createValidBooking.json");
      }

      public static UpdateBookingRequestModel UpdateValidBookingFromJson()
      {
            return JsonTestDataReader.LoadJsonFile<UpdateBookingRequestModel>("updateValidBooking.json");
      }
}