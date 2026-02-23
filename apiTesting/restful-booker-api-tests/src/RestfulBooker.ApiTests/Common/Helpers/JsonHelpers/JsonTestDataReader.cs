using System.Text.Json;

namespace RestfulBooker.ApiTests.Common;

public static class JsonTestDataReader
{
      public static T LoadJsonFile<T>(string fileName)
      {
            var path = Path.Combine(AppContext.BaseDirectory, "Common", "Helpers", "JsonTestData", fileName);

            if (!File.Exists(path))
                  throw new FileNotFoundException($"Test data file not found: {path}");

            var json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<T>(json, JsonSerializerHelper.Options)!;
      }
}