namespace RestfulBooker.ApiTests.Common;

public static class Constants
{
      #region Error messages:
      public static string InvalidAuthorizationToken = "invalid_authorization_token";
      #endregion


      #region Database
      public const string DatabaseFile = "bookings.db";
      #endregion


      #region Schemas:
      public const string SchemaPath = "TestData/CreateBookingSchema.json";
      #endregion


      #region Endpoints:
      public const string BookingEndpoint = "/booking";
      public static string BookingById(int id) => $"{BookingEndpoint}/{id}";
      public const string AuthEndpoint = "/auth";
      public const string HealthCheck = "/ping";
      #endregion


      #region General request properties:
      public const string Accept = "Accept";
      public const string ApplicationJson = "application/json";
      public const string ContentType = "content-type";
      public const string Cookie = "Cookie";
      public static string TokenValue(string token) => $"token={token}";
      #endregion


      #region Authentication:
      public const string AuthUsername = "Auth:Username";
      public const string AuthPassword = "Auth:Password";
      #endregion
}