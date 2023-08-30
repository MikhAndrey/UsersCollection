using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using UsersCollectionAPI.Model.Dto;
using UsersCollectionAPI.Utils;

namespace UsersCollectionConsole;

public class UserHttpManager
{
    private const string BaseApiUrl = "https://localhost:7200/";
    private const string UserCreateEndpoint = "Auth/CreateUser";
    private const string UserRemoveEndpoint = "Auth/RemoveUser";
    private const string SetStatusEndpoint = "Auth/SetStatus";
    private readonly Func<int, string> _getUserInfoEndpoint = id => $"Public/UserInfo?id={id}";
    
    private const string JsonFormatHeaderAlias = "application/json";
    private const string XmlFormatHeaderAlias = "application/xml";
    private const string FormUrlEncodedFormatHeaderAlias = "application/x-www-form-urlencoded";

    private const string FormUrlEncodedUserIdAlias = "Id";
    private const string FormUrlEncodedNewStatusAlias = "NewStatus";

    public async Task CreateUser(UserCreateXmlRequestDto user)
    {
        const string endpoint = BaseApiUrl + UserCreateEndpoint;
        HttpClient client = new HttpClient().ConfigureBasicAuthHeader(endpoint);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(XmlFormatHeaderAlias));
        
        string xml = new CustomXmlSerializer<UserCreateXmlRequestDto>().Serialize(user);
        
        HttpContent body = new StringContent(xml, Encoding.UTF8, XmlFormatHeaderAlias);
        string bodyAsString = await body.ReadAsStringAsync();
        Console.WriteLine($"Request:\n{bodyAsString}");
        HttpResponseMessage response = await client.PostAsync(endpoint, body);
        
        string responseXml = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response:\n{responseXml}");
    }
    
    public async Task RemoveUser(UserRemoveDto user)
    {
        const string endpoint = BaseApiUrl + UserRemoveEndpoint;
        HttpClient client = new HttpClient().ConfigureBasicAuthHeader(endpoint);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonFormatHeaderAlias));
        
        string requestBody = JsonSerializer.Serialize(user);
        Console.WriteLine($"Request:\n{requestBody}");
        HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, user);
        
        string responseAsString = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response:\n{responseAsString}");
    }
    
    public async Task GetUserById(int id)
    {
        string endpoint = BaseApiUrl + _getUserInfoEndpoint(id);
        HttpClient client = new HttpClient().ConfigureBasicAuthHeader(endpoint);

        HttpResponseMessage response = await client.GetAsync(endpoint);
        
        string responseAsString = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response:\n{responseAsString}");
    }

    public async Task SetStatus(int userId, string status)
    {
        const string endpoint = BaseApiUrl + SetStatusEndpoint;
        HttpClient client = new HttpClient().ConfigureBasicAuthHeader(endpoint);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(FormUrlEncodedFormatHeaderAlias));
        
        FormUrlEncodedContent content = new (new Dictionary<string, string>
        {
            { FormUrlEncodedUserIdAlias, userId.ToString() },
            { FormUrlEncodedNewStatusAlias, status }
        });
        HttpResponseMessage response = await client.PostAsync(endpoint, content);

        string responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
    }
}
