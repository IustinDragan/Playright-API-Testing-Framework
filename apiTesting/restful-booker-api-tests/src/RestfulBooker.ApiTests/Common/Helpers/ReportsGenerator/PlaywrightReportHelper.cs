namespace RestfulBooker.ApiTests.Helpers;

public static class PlaywrightReportHelper
{
      private static string ProjectRoot => GetProjectRoot();
      private static string TestResultsDir => Path.Combine(ProjectRoot, "TestResults");
      private static string PlaywrightReportDir => Path.Combine(ProjectRoot, "playwright-report");


      private static string GetProjectRoot()
      {
            // Navigate from bin/Debug/net10.0 to project root
            // Using relative path navigation: ../../..
            string binPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..");
            return Path.GetFullPath(binPath);
      }

      public static async Task OrganizeReportAsync()
      {
            try
            {
                  string timestamp = DateTime.Now.ToString("yyyyMMddTHHmmss");
                  string timestampedFolder = Path.Combine(TestResultsDir, $"Deploy_{timestamp}");

                  // Create the timestamped directory structure
                  Directory.CreateDirectory(timestampedFolder);

                  // Move Playwright reports if they exist
                  if (Directory.Exists(PlaywrightReportDir))
                  {
                        string playwrightSubDir = Path.Combine(timestampedFolder, "Playwright");
                        Directory.CreateDirectory(playwrightSubDir);
                        await CopyDirectoryAsync(PlaywrightReportDir, playwrightSubDir);
                        Directory.Delete(PlaywrightReportDir, recursive: true);
                  }

                  // Move SpecFlow reports if they exist
                  MoveSpecFlowReports(timestampedFolder);
            }
            catch (Exception ex)
            {
                  Console.WriteLine($"Error organizing test reports: {ex.Message}");
            }
      }
      private static void MoveSpecFlowReports(string timestampedFolder)
      {
            try
            {
                  string livingDocDir = Path.Combine(GetProjectRoot(), "LivingDoc");
                  if (Directory.Exists(livingDocDir))
                  {
                        string specflowSubDir = Path.Combine(timestampedFolder, "SpecFlowReport");
                        Directory.CreateDirectory(specflowSubDir);
                        CopyDirectoryAsync(livingDocDir, specflowSubDir).GetAwaiter().GetResult();
                  }
            }
            catch (Exception ex)
            {
                  Console.WriteLine($"Warning: Could not move SpecFlow reports: {ex.Message}");
            }
      }

      private static async Task CopyDirectoryAsync(string sourceDir, string destinationDir)
      {
            var source = new DirectoryInfo(sourceDir);

            foreach (var file in source.GetFiles())
            {
                  try
                  {
                        string destFilePath = Path.Combine(destinationDir, file.Name);
                        file.CopyTo(destFilePath, overwrite: true);
                  }
                  catch (Exception ex)
                  {
                        Console.WriteLine($"Warning: Could not copy file {file.Name}: {ex.Message}");
                  }
            }

            foreach (var dir in source.GetDirectories())
            {
                  string destDirPath = Path.Combine(destinationDir, dir.Name);
                  Directory.CreateDirectory(destDirPath);
                  await CopyDirectoryAsync(dir.FullName, destDirPath);
            }

            await Task.CompletedTask;
      }
}