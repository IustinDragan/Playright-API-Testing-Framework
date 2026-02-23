using System.Text.Json;
using System.Text.Json.Serialization;

namespace RestfulBooker.ApiTests.Common;

public static class JsonSerializerHelper
{
      public static readonly JsonSerializerOptions Options = new()
      {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
      };
}