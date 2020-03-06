using System.IO;
using AcceptanceTests.Common.Data.Helpers;

namespace AcceptanceTests.Common.Data.TestData
{
    public static class LoadXmlFile
    {
        public static CommonData SerialiseCommonData(string path = "Data/TestData/CommonData.xml")
        {
            var xmlInputData = File.ReadAllText(path);
            return XmlSerialiser.Deserialize<CommonData>(xmlInputData);
        }
    }
}
