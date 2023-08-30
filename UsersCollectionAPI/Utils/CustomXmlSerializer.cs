using System.Xml.Serialization;

namespace UsersCollectionAPI.Utils;

public class CustomXmlSerializer<T> where T: class
{
    public string Serialize(T elem){
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StringWriter stringWriter = new Utf8StringWriter();
        serializer.Serialize(stringWriter, elem);
        string xmlString = stringWriter.ToString();
        return xmlString;
    }
}
