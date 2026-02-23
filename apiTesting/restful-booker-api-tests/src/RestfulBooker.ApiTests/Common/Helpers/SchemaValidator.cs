using Gherkin.CucumberMessages.Types;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace RestfulBooker.ApiTests.Common;

public static class SchemaValidator
{
      public static void Validate(string schemaPath, string jsonRespone)
      {
            var schema = JSchema.Parse(File.ReadAllText(schemaPath));
            var json = JObject.Parse(jsonRespone);

            Assert.IsTrue(json.IsValid(schema), "Schema validation failed.");
      }
}