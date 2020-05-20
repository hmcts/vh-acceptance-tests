using System;
using System.IO;

namespace AcceptanceTests.Common.AudioRecordings
{
    public static class AudioRecordingsManager
    {
        public static string CreateNewAudioFile(string originalFileName, Guid hearingId, string path = "TestAudioFiles")
        {
            var originalFile = $"{path}/{originalFileName}";
            var newFileName = $"{path}/{hearingId}.mp4";
            File.Move($"{originalFile}", newFileName);
            return newFileName;
        }

        public static void RemoveLocalAudioFile(string filename)
        {
            File.Delete(filename);
        }
    }
}
