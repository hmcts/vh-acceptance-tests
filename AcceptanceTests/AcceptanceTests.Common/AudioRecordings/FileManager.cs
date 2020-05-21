using System;
using System.IO;
using System.Reflection;

namespace AcceptanceTests.Common.AudioRecordings
{
    public static class FileManager
    {
        public static string GetAssemblyDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        public static string CreateNewAudioFile(string originalFileName, Guid hearingId, string path = "TestAudioFiles")
        {
            var originalFilePath = Path.Join(GetAssemblyDirectory(), path, originalFileName);
            if (!File.Exists(originalFilePath))
            {
                throw new FileNotFoundException($"Unable to find audio file with path : {originalFilePath}");
            }

            var fileWithExtension = $"{hearingId}.mp4";
            var newFilePath = Path.Join(path, fileWithExtension);
            File.Copy(originalFilePath, newFilePath, true);
            return newFilePath;
        }

        public static void RemoveLocalAudioFile(string filepath)
        {
            var fileAndDirectory = Path.Join(GetAssemblyDirectory(), filepath);
            if (!File.Exists(fileAndDirectory))
            {
                throw new FileNotFoundException($"Unable to find audio file with path : {fileAndDirectory}");
            }
            File.Delete(fileAndDirectory);
        }
    }
}
