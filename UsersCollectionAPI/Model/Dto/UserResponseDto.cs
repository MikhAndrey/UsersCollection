using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace UsersCollectionAPI.Model.Dto;

[XmlRoot("Response")]
public class UserResponseDto
{
    [XmlAttribute("Success")]
    [JsonPropertyName("Success")]
    public bool Success { get; set; }
    
    [XmlAttribute("ErrorId")]
    [JsonPropertyName("ErrorId")]
    public int ErrorId { get; set; }
    
    [XmlElement("user")]
    [JsonPropertyName("user")]
    public UserRequestDto? User { get; set; }
    
    [XmlElement("ErrorMsg")]
    [JsonPropertyName("Msg")]
    public string? Message { get; set; }
}
