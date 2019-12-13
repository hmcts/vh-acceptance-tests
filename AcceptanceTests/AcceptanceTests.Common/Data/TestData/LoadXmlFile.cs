using System.IO;
using AcceptanceTests.Common.Data.Helpers;

namespace AcceptanceTests.Common.Data.TestData
{
    public class LoadXmlFile
    {
        public CommonData SerialiseCommonData(string path = "Data/TestData/CommonData.xml")
        {
            var serialiser = new XmlSerialiser();
            var xmlInputData = File.ReadAllText(path);
            return serialiser.Deserialize<CommonData>(xmlInputData);
        }
    }
}
