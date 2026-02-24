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

      #region Create Booking Methods
      public static CreateBookingRequestModel ValidBookingWithFirstName(string firstname)
      {
            var booking = ValidBookingFromJson();
            booking.Firstname = firstname;
            return booking;
      }

      public static CreateBookingRequestModel ValidBookingWithLastName(string lastname)
      {
            var booking = ValidBookingFromJson();
            booking.Lastname = lastname;
            return booking;
      }

      public static CreateBookingRequestModel ValidBookingWithPrice(int price)
      {
            var booking = ValidBookingFromJson();
            booking.Totalprice = price;
            return booking;
      }

      public static CreateBookingRequestModel ValidBookingWithAdditionalNeeds(string additionalneeds)
      {
            var booking = ValidBookingFromJson();
            booking.Additionalneeds = additionalneeds;
            return booking;
      }

      public static CreateBookingRequestModel ValidBookingWithDates(string checkin, string checkout)
      {
            var booking = ValidBookingFromJson();
            booking.Bookingdates.Checkin = checkin;
            booking.Bookingdates.Checkout = checkout;

            return booking;
      }
      #endregion

      #region Update Booking Methods

      public static UpdateBookingRequestModel UpdateBookingWithFirstName(string firstname)
      {
            var booking = UpdateValidBookingFromJson();
            booking.Firstname = firstname;
            return booking;
      }

      public static UpdateBookingRequestModel UpdateBookingWithPrice(int price)
      {
            var booking = UpdateValidBookingFromJson();
            booking.Totalprice = price;
            return booking;
      }

      public static UpdateBookingRequestModel UpdateBookingWithAdditionalNeeds(string additionalneeds)
      {
            var booking = UpdateValidBookingFromJson();
            booking.Additionalneeds = additionalneeds;
            return booking;
      }

      public static UpdateBookingRequestModel UpdateBookingWithDates(string checkin, string checkout)
      {
            var booking = UpdateValidBookingFromJson();
            booking.Bookingdates.Checkin = checkin;
            booking.Bookingdates.Checkout = checkout;

            return booking;
      }

      #endregion
}