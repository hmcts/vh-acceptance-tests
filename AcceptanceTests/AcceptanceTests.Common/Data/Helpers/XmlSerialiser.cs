﻿using System.IO;
using System.Xml.Serialization;

namespace AcceptanceTests.Common.Data.Helpers
{
    public static class XmlSerialiser
    {
        public static T Deserialize<T>(string input) where T : class
        {
            var ser = new XmlSerializer(typeof(T));

            using var sr = new StringReader(input);
            return (T)ser.Deserialize(sr);
        }

        public static string Serialize<T>(T objectToSerialize)
        {
            var xmlSerializer = new XmlSerializer(objectToSerialize.GetType());

            using var textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, objectToSerialize);
            return textWriter.ToString();
        }
    }
}
