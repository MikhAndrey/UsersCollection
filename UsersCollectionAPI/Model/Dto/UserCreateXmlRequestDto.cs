using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace UsersCollectionAPI.Model.Dto;

[XmlRoot("Request")]
public class UserCreateXmlRequestDto
{
    [XmlElement("user")]
    public required UserRequestDto User { get; set; }
}

public class UserRequestDto
{
    [XmlAttribute("Id")]
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    
    [XmlAttribute("Name")]
    [JsonPropertyName("Name")]
    public required string Name { get; set; }
    
    [XmlElement("Status")]
    [JsonPropertyName("Status")]
    public required string Status { get; set; }
}
