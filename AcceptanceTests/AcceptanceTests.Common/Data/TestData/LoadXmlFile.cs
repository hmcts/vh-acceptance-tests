using System.IO;
using AdminWebsite.Common.Data.Helpers;

namespace AdminWebsite.Common.Data.TestData
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
