using System.Text.Json;
using RestfulBooker.ApiTests.API;

namespace RestfulBooker.ApiTests.Common;

public static class BookingAssertions
{
      public static void AssertBookingUpdate(JsonElement booking, UpdateBookingRequestModel expectedBooking)
      {
            Assert.AreEqual(expectedBooking.Firstname, booking.GetProperty("firstname").GetString(), "Firstname was not updated correctly");
            Assert.AreEqual(expectedBooking.Lastname, booking.GetProperty("lastname").GetString(), "Lastname was not updated correctly");
            Assert.AreEqual(expectedBooking.Totalprice, booking.GetProperty("totalprice").GetInt32(), "Total price was not updated correctly");
            Assert.AreEqual(expectedBooking.Depositpaid, booking.GetProperty("depositpaid").GetBoolean(), "Deposit paid was not updated correctly");

            var dates = booking.GetProperty("bookingdates");

            Assert.AreEqual(expectedBooking.Bookingdates.Checkin, dates.GetProperty("checkin").GetString(), "Check-in date was not updated correctly");
            Assert.AreEqual(expectedBooking.Bookingdates.Checkout, dates.GetProperty("checkout").GetString(), "Check-out date was not updated correctly");
      }

      public static void ValidateStatus(int expected, int actual) => Assert.AreEqual(expected, actual, $"Expected status code: {expected} but was: {actual}");

      public static void ValidateBookingId(CreateBookingResponseModel model) => Assert.IsTrue(model.BookingId > 0, "BookingId was not returned or invalid.");
}