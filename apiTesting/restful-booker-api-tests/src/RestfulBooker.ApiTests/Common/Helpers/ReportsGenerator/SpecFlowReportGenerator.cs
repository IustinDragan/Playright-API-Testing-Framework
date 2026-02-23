using System.Text;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Helpers;

/// <summary>
/// Generates SpecFlow test execution reports in HTML format
/// Reports are organized in TestResults/{timestamp}/SpecFlowReport/
/// </summary>
public static class SpecFlowReportGenerator
{
      private static List<TestScenarioResult> _scenarioResults = new();
      private static DateTime _testRunStartTime = DateTime.Now;

      /// <summary>
      /// Records a scenario result for reporting
      /// </summary>

      public static void RecordScenarioResult(ScenarioContext scenarioContext, string stepHistoryJson = "")
      {
            var result = new TestScenarioResult
            {
                  ScenarioName = scenarioContext.ScenarioInfo?.Title ?? "Unknown Scenario",
                  FeatureName = "Booking Management",
                  Status = scenarioContext.TestError == null ? "Passed" : "Failed",
                  ErrorMessage = scenarioContext.TestError?.Message ?? string.Empty,
                  ExecutionTime = DateTime.Now
            };

            _scenarioResults.Add(result);
      }

      /// <summary>
      /// Generates an HTML report from collected test results
      /// </summary>
      public static async Task GenerateReportAsync()
      {
            try
            {
                  string timestamp = DateTime.Now.ToString("yyyyMMddTHHmmss");

                  // Navigate from bin/Debug/net10.0 to project root
                  // e.g., from: /project/bin/Debug/net10.0
                  // to: /project/TestResults
                  string binPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..");
                  binPath = Path.GetFullPath(binPath);

                  string testResultsDir = Path.Combine(binPath, "TestResults");
                  string reportDir = Path.Combine(testResultsDir, $"Deploy_{timestamp}", "SpecFlowReport");

                  Directory.CreateDirectory(reportDir);

                  string htmlContent = GenerateHtmlReport();
                  string reportPath = Path.Combine(reportDir, "index.html");

                  await File.WriteAllTextAsync(reportPath, htmlContent, Encoding.UTF8);

                  Console.WriteLine($"SpecFlow report generated: {reportPath}");
            }
            catch (Exception ex)
            {
                  Console.WriteLine($"Error generating SpecFlow report: {ex.Message}");
            }
      }

      /// <summary>
      /// Generates HTML content for the test report
      /// </summary>
      private static string GenerateHtmlReport()
      {
            int passedCount = 0;
            int failedCount = 0;

            foreach (var result in _scenarioResults)
            {
                  if (result.Status == "Passed")
                        passedCount++;
                  else
                        failedCount++;
            }

            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"UTF-8\">");
            sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("    <title>SpecFlow Test Report</title>");
            sb.AppendLine("    <style>");
            sb.AppendLine(GenerateCss());
            sb.AppendLine("    </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("    <div class=\"container\">");
            sb.AppendLine("        <header>");
            sb.AppendLine("            <h1>SpecFlow Test Execution Report</h1>");
            sb.AppendLine("            <p class=\"timestamp\">Report Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</p>");
            sb.AppendLine("        </header>");

            // Summary Section
            sb.AppendLine("        <section class=\"summary\">");
            sb.AppendLine("            <h2>Test Summary</h2>");
            sb.AppendLine("            <div class=\"stats\">");
            sb.AppendLine($"                <div class=\"stat-box total\"><strong>Total Tests:</strong> {_scenarioResults.Count}</div>");
            sb.AppendLine($"                <div class=\"stat-box passed\"><strong>Passed:</strong> {passedCount}</div>");
            sb.AppendLine($"                <div class=\"stat-box failed\"><strong>Failed:</strong> {failedCount}</div>");
            sb.AppendLine($"                <div class=\"stat-box success-rate\"><strong>Success Rate:</strong> {(_scenarioResults.Count > 0 ? (passedCount * 100 / _scenarioResults.Count) : 0)}%</div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("        </section>");

            // Test Details Section
            sb.AppendLine("        <section class=\"details\">");
            sb.AppendLine("            <h2>Test Details</h2>");
            sb.AppendLine("            <table class=\"results-table\">");
            sb.AppendLine("                <thead>");
            sb.AppendLine("                    <tr>");
            sb.AppendLine("                        <th>Feature</th>");
            sb.AppendLine("                        <th>Scenario</th>");
            sb.AppendLine("                        <th>Status</th>");
            sb.AppendLine("                        <th>Time</th>");
            sb.AppendLine("                    </tr>");
            sb.AppendLine("                </thead>");
            sb.AppendLine("                <tbody>");

            foreach (var result in _scenarioResults)
            {
                  string statusClass = result.Status == "Passed" ? "passed" : "failed";
                  sb.AppendLine("                    <tr class=\"" + statusClass + "\">");
                  sb.AppendLine("                        <td>" + HtmlEncode(result.FeatureName!) + "</td>");
                  sb.AppendLine("                        <td>" + HtmlEncode(result.ScenarioName!) + "</td>");
                  sb.AppendLine("                        <td class=\"status-badge " + statusClass + "\">" + result.Status + "</td>");
                  sb.AppendLine("                        <td>" + result.ExecutionTime.ToString("HH:mm:ss") + "</td>");
                  sb.AppendLine("                    </tr>");

                  if (!string.IsNullOrEmpty(result.ErrorMessage))
                  {
                        sb.AppendLine("                    <tr class=\"error-row\">");
                        sb.AppendLine("                        <td colspan=\"4\" class=\"error-message\">");
                        sb.AppendLine("                            <strong>Error:</strong> " + HtmlEncode(result.ErrorMessage));
                        sb.AppendLine("                        </td>");
                        sb.AppendLine("                    </tr>");
                  }
            }

            sb.AppendLine("                </tbody>");
            sb.AppendLine("            </table>");
            sb.AppendLine("        </section>");

            sb.AppendLine("    </div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
      }

      /// <summary>
      /// Generates CSS styles for the HTML report
      /// </summary>
      private static string GenerateCss()
      {
            return @"
                * { margin: 0; padding: 0; box-sizing: border-box; }
                body { font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif; background: #f5f5f5; color: #333; }
                .container { max-width: 1200px; margin: 0 auto; padding: 20px; }
                header { background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px; border-radius: 8px 8px 0 0; margin-bottom: 20px; }
                header h1 { font-size: 28px; margin-bottom: 8px; }
                .timestamp { font-size: 14px; opacity: 0.9; }
                section { background: white; margin-bottom: 20px; padding: 25px; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
                section h2 { color: #333; margin-bottom: 20px; font-size: 20px; border-bottom: 2px solid #667eea; padding-bottom: 10px; }
                .stats { display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 15px; }
                .stat-box { padding: 15px; border-radius: 6px; color: white; font-weight: 500; text-align: center; }
                .stat-box.total { background: #5a67d8; }
                .stat-box.passed { background: #48bb78; }
                .stat-box.failed { background: #f56565; }
                .stat-box.success-rate { background: #ed8936; }
                .results-table { width: 100%; border-collapse: collapse; }
                .results-table thead { background: #f7f7f7; }
                .results-table th { padding: 12px; text-align: left; font-weight: 600; border-bottom: 2px solid #e2e8f0; }
                .results-table tr:hover { background: #f9f9f9; }
                .results-table td { padding: 12px; border-bottom: 1px solid #e2e8f0; }
                .status-badge { display: inline-block; padding: 4px 12px; border-radius: 20px; font-size: 12px; font-weight: 600; }
                .status-badge.passed { background: #c6f6d5; color: #22543d; }
                .status-badge.failed { background: #fed7d7; color: #742a2a; }
                .error-row { background: #fff5f5; }
                .error-message { color: #c53030; font-size: 13px; padding: 10px; background: #fff5f5; border-left: 4px solid #c53030; }
            ";
      }

      /// <summary>
      /// HTML encodes a string for safe display
      /// </summary>
      private static string HtmlEncode(string text)
      {
            if (string.IsNullOrEmpty(text)) return text;
            return text.Replace("&", "&amp;")
                      .Replace("<", "&lt;")
                      .Replace(">", "&gt;")
                      .Replace("\"", "&quot;")
                      .Replace("'", "&#39;");
      }

      /// <summary>
      /// Internal class to store scenario results
      /// </summary>
      private class TestScenarioResult
      {
            public string? FeatureName { get; set; }
            public string? ScenarioName { get; set; }
            public string? Status { get; set; }
            public string? ErrorMessage { get; set; }
            public DateTime ExecutionTime { get; set; }
      }
}