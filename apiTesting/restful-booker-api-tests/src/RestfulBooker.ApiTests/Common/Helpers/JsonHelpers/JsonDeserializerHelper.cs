using System.Text.Json;
using Microsoft.Playwright;

namespace RestfulBooker.ApiTests.Common;

public static class JsonDeserializerHelper
{
      public static async Task<T> DeserializeAsync<T>(this IAPIResponse response)
      {
            var body = await response.TextAsync();

            var model = JsonSerializer.Deserialize<T>(body, JsonSerializerHelper.Options);

            if (model is null)
                  throw new JsonException($"Failed to deserialize response to {typeof(T).Name}. Body: {body}");

            return model;
      }

      public static void ValidateJsonFormat(string ContentType, string body)
      {
            Assert.IsTrue(ContentType.Contains(Constants.ApplicationJson));
            JsonDocument.Parse(body);

      }
}