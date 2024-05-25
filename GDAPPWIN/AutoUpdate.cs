using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.IO.Compression;

namespace GDAPPWIN
{
    public class AutoUpdate
    {
        public void PerformUpdate()
        {
            // URL of the update file
            string updateUrl = "http://192.168.10.198/update/update.zip";

            // Name of the update file
            string updateFileName = "update.zip";

            // Path to save the update file
            string updateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, updateFileName);

            try
            {
                // Download the update file
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(updateUrl, updateFilePath);
                }

                // Create a directory to extract the update files
                string updateDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UpdateFiles");
                Directory.CreateDirectory(updateDirectory);

                // Extract the update files
                ZipFile.ExtractToDirectory(updateFilePath, updateDirectory);

                // Path to the installer executable
                string installerPath = Path.Combine(updateDirectory, "setup.exe");

                // Execute the installer
                RunInstaller(installerPath, updateDirectory);

                // Display a message when the update is completed
                Console.WriteLine("Update downloaded and installed successfully.");
            }
            catch (Exception ex)
            {
                // Display an error message if an error occurs
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void RunInstaller(string installerPath, string workingDirectory)
        {
            // Create ProcessStartInfo object
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = installerPath; // Path to the installer executable
            startInfo.WorkingDirectory = workingDirectory; // Working directory for the installer
            startInfo.UseShellExecute = true;

            // Start the process
            Process.Start(startInfo);
        }
    }
}
