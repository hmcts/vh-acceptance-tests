using System;
using System.IO;
using System.Reflection;

namespace AcceptanceTests.Common.AudioRecordings
{
    public static class AudioRecordingsManager
    {
        public static string CreateNewAudioFile(string originalFileName, Guid hearingId, string path = "TestAudioFiles")
        {
            var originalFile = $"{GetAssemblyDirectory()}\\{path}\\{originalFileName}";
            if (!File.Exists(originalFile))
            {
                throw new FileNotFoundException($"Unable to find audio file with path : {originalFile}. ");
            }

            var newFileName = $"{path}\\{hearingId}.mp4";
            File.Move($"{originalFile}", newFileName);
            return newFileName;
        }

        public static void RemoveLocalAudioFile(string filename)
        {
            var fileAndDirectory = $"{GetAssemblyDirectory()}\\{filename}";
            if (!File.Exists(fileAndDirectory))
            {
                throw new FileNotFoundException($"Unable to find audio file with path : {fileAndDirectory}");
            }
            File.Delete(fileAndDirectory);
        }

        public static string GetAssemblyDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
