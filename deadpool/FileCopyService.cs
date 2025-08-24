using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace deadpool;

public class FileCopyService
{
    public async Task<bool> CopyFileToServerAsync(string sourcePath, string destinationServer,
        string destinationPath, string? username = null, string? password = null,
        IProgress<int>? progress = null, CancellationToken cancellationToken = default)
    {
        try
        {
            // Validate inputs
            if (string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
                throw new FileNotFoundException("Source file not found", sourcePath);

            if (string.IsNullOrEmpty(destinationServer))
                throw new ArgumentException("Destination server cannot be empty");

            // Construct UNC path
            string uncPath = $@"\\{destinationServer}\{destinationPath.TrimStart('\\')}";

            // Ensure destination directory exists
            string destinationDirectory = Path.GetDirectoryName(uncPath);
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            // Copy file with progress reporting
            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            using (FileStream destStream = new FileStream(uncPath, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[81920];
                long totalBytes = sourceStream.Length;
                long totalRead = 0;
                int bytesRead;

                while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                {
                    await destStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                    totalRead += bytesRead;

                    // Report progress
                    if (progress != null && totalBytes > 0)
                    {
                        int percentage = (int)(totalRead * 100 / totalBytes);
                        progress.Report(percentage);
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            // Log error (you might want to use a proper logging framework)
            Console.WriteLine($"Error copying file: {ex.Message}");
            return false;
        }
    }
}