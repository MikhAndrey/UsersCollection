using System.Net.Http.Headers;
using System.Text;

namespace UsersCollectionConsole;

public static class HttpClientExtension
{
    private const string BasicAuthIndicator = "/Auth/";
    
    public static HttpClient ConfigureBasicAuthHeader(this HttpClient client, string endpoint)
    {
        if (endpoint.Contains(BasicAuthIndicator))
        {
            string authString = $"{App.UserName}:{App.UserPassword}";
            string base64AuthString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authString));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64AuthString);
        }
        return client;
    }
}
