using System;
using System.IO;
using System.Reflection;

namespace AcceptanceTests.Common.AudioRecordings
{
    public static class AudioRecordingsManager
    {
        public static string CreateNewAudioFile(string originalFileName, Guid hearingId, string path = "TestAudioFiles")
        {
            var originalFile = $"{path}\\{originalFileName}";
            if (!File.Exists(originalFile))
            {
                throw new FileNotFoundException($"Unable to find audio file with path : {Assembly.GetExecutingAssembly().Location}\\{originalFile}. ");
            }

            var newFileName = $"{path}\\{hearingId}.mp4";
            File.Move($"{originalFile}", newFileName);
            return newFileName;
        }

        public static void RemoveLocalAudioFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"Unable to find audio file with path : {Assembly.GetExecutingAssembly().Location}\\{filename}");
            }
            File.Delete(filename);
        }
    }
}
