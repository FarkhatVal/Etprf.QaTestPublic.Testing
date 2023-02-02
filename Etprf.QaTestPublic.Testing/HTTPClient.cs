using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using RestSharp;

namespace Etprf.QaTestPublic.Testing.Etprf.QaTestPublic.Testing;

public class HTTPClient
{
    public static async Task<string?> GetSecretHeaderValue(string Host1, string ApiGetSecret, string importantDataValue)
    {
        var addressGetSecret = new Uri(Host1 + ApiGetSecret);
        var client = new HttpClient() { BaseAddress = addressGetSecret } ;
        var response = await client.GetAsync(addressGetSecret, new CancellationToken());
        if (response.StatusCode != HttpStatusCode.OK)
        {
            Assert.Fail($"{Host1} с запросом important = {importantDataValue} отработала некорректно, дальнейшие действия бессмысленны!");
        }
        var secretValue = response.Headers.GetValues("Secret").FirstOrDefault();
        return secretValue;
    }

    public static async Task<string?> GetFinalResult(string Host2, string secretHeaderValue, string specialDataValue)
    {
        var client = new RestClient(Host2);
        var request = new RestRequest($"?query string={secretHeaderValue}", Method.Post);
        request.AddJsonBody($"{{\"special\": {specialDataValue}}}");
        var response = client.Execute(request);
        Debug.Assert(response.Content != null, "response.Content = null");
        Debug.Assert(response.Content != "", "response.Content is empty");
        var finalResult = Regex.Replace(response.Content, "[^0-9]", "");
        return finalResult;
    }
    
    public static async Task<string?> GetSecretHeaderValueWithHeaderImportant(string Host1,string customHeader, string importantDataValue)
    {
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add(customHeader, importantDataValue);
        var response = await httpClient.GetAsync(Host1, new CancellationToken());
        if (response.StatusCode != HttpStatusCode.OK)
        {
            Assert.Fail($"{Host1} с запросом important = {importantDataValue} отработала некорректно, дальнейшие действия бессмысленны!");
        }
        var secretValue = response.Headers.GetValues("Secret").FirstOrDefault();
        return secretValue;
    }
}