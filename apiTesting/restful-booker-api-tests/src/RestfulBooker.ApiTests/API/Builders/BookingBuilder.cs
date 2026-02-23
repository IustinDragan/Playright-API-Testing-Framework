
namespace RestfulBooker.ApiTests.API.Builders;

public class BookingBuilder
{
      private string? _firstName;
      private string? _lastName;
      private int? _totalPrice;
      private bool? _depositPaid;
      private BookingDates? _bookingDates;
      private string? _additionalNeeds;

      public BookingBuilder WithFirstName(string firstName)
      {
            _firstName = firstName;
            return this;
      }

      public BookingBuilder WithLastName(string lastName)
      {
            _lastName = lastName;
            return this;
      }

      public BookingBuilder WithTotalPrice(int totalPrice)
      {
            _totalPrice = totalPrice;
            return this;
      }

      public BookingBuilder WithDepositPaid(bool depositPaid)
      {
            _depositPaid = depositPaid;
            return this;
      }
      public BookingBuilder WithBookingDates(DateTime checkin, DateTime checkout)
      {
            _bookingDates = new BookingDates
            {
                  Checkin = checkin.ToString("yyyy-MM-dd"),
                  Checkout = checkout.ToString("yyyy-MM-dd")
            };
            return this;
      }

      public BookingBuilder WithAdditionalNeeds(string additionalNeeds)
      {
            _additionalNeeds = additionalNeeds;
            return this;
      }

      public CreateBookingRequestModel Build()
      {
            return new CreateBookingRequestModel
            {
                  Firstname = _firstName!,
                  Lastname = _lastName!,
                  Totalprice = _totalPrice!.Value,
                  Depositpaid = _depositPaid!.Value,
                  Bookingdates = _bookingDates!,
                  Additionalneeds = _additionalNeeds!
            };
      }
}
