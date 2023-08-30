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

    public T Deserialize(string xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using StringReader reader = new StringReader(xml);
        T response = (T)serializer.Deserialize(reader)!;
        return response;
    }
}
