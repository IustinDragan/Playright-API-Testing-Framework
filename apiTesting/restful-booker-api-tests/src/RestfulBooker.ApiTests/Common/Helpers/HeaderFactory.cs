namespace RestfulBooker.ApiTests.Common;

public static class HeaderFactory
{
      public static Dictionary<string, string> DefaulHeaders()
      {
            return new Dictionary<string, string>
            {
                  {Constants.ContentType, Constants.ApplicationJson},
                  {Constants.Accept, Constants.ApplicationJson}
            };
      }

      public static Dictionary<string, string> AuthHeaders(string token)
      {
            return new Dictionary<string, string>
            {
                  {Constants.Cookie, Constants.TokenValue(token)}
            };
      }
}