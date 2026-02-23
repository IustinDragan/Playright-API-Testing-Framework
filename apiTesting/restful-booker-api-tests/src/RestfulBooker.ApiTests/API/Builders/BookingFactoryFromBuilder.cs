using System.Security.Cryptography;

namespace RestfulBooker.ApiTests.API.Builders;

public static class BookingFactoryFromBuilder
{
      public static BookingBuilder ValidBooking()
      {
            return new BookingBuilder()
              .WithFirstName("Joe" + RandomNumberGenerator.GetString("0123456789", 3))
              .WithLastName("Joe" + RandomNumberGenerator.GetString("0123456789", 3))
              .WithTotalPrice(987)
              .WithDepositPaid(true)
              .WithBookingDates(DateTime.Now, DateTime.Now.AddDays(5))
              .WithAdditionalNeeds("coffee");
      }

      public static BookingBuilder InvalidBooking()
      {
            return new BookingBuilder()
              .WithFirstName(null!)
              .WithLastName(null!)
              .WithTotalPrice(-987)
              .WithDepositPaid(false)
              .WithBookingDates(DateTime.Now, DateTime.Now.AddDays(5))
              .WithAdditionalNeeds(null!);
      }
}