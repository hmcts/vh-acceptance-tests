using System;
using System.IO;

namespace AcceptanceTests.Common.AudioRecordings
{
    public static class AudioRecordingsManager
    {
        public static string CreateNewAudioFile(string originalFileName, Guid hearingId, string path = "TestAudioFiles")
        {
            if (!File.Exists($"{path}/{originalFileName}"))
            {
                throw new FileNotFoundException($"Unable to find audio file with path : {path}/{originalFileName}");
            }

            var newFileName = $"{path}/{hearingId}.mp4";
            File.Move(originalFileName, newFileName);
            return newFileName;
        }
    }
}
