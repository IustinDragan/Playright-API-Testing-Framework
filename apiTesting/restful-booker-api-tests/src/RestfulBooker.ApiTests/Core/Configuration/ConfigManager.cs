#region Just for learning purpose. I have already implemented DI which is a better way than ConfigManager

// using System.Reflection;
// using System.Text.Json;

// namespace RestfulBooker.ApiTests.Core;

// public static class ConfigManager
//     {
//         private static readonly Dictionary<string, string> _config;

//         static ConfigManager()
//         {
//             var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
//             var configPath = Path.Combine(assemblyLocation, "appsettings.json");
//             var json = File.ReadAllText(configPath);
//             _config = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
//         }

//         public static string BaseUrl => _config["BaseUrl"];
//         public static string Username => _config["Username"];
//         public static string Password => _config["Password"];
//         public static string DatabasePath => _config["DatabasePath"];
//     }

#endregion