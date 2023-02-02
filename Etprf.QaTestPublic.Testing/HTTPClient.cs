using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using RestSharp;

namespace Etprf.QaTestPublic.Testing.Etprf.QaTestPublic.Testing;

public class HTTPClient
{
    public static async Task<string?> GetSecretHeaderValue(string host1, string apiGetSecret, string importantDataValue)
    {
        var addressGetSecret = new Uri(host1 + apiGetSecret);
        var client = new HttpClient() { BaseAddress = addressGetSecret } ;
        var response = await client.GetAsync(addressGetSecret, new CancellationToken());
        if (response.StatusCode != HttpStatusCode.OK)
        {
            Assert.Fail($"{host1} с запросом important = {importantDataValue} отработала некорректно, дальнейшие действия бессмысленны!");
        }
        var secretValue = response.Headers.GetValues("Secret").FirstOrDefault();
        return secretValue;
    }

    public static async Task<string> GetFinalResult(string host2, string secretHeaderValue, string specialDataValue)
    {
        var client = new RestClient(host2);
        var request = new RestRequest($"?query string={secretHeaderValue}", Method.Post);
        request.AddJsonBody($"{{\"special\": {specialDataValue}}}");
        var response = client.Execute(request);
        Debug.Assert(response.Content != null, "response.Content = null");
        Debug.Assert(response.Content != "", "response.Content is empty");
        var finalResult = Regex.Replace(response.Content, "[^0-9]", "");
        return finalResult;
    }
    
    public static async Task<string?> GetSecretHeaderValueWithHeaderImportant(string host1,string customHeader, string importantDataValue)
    {
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add(customHeader, importantDataValue);
        var response = await httpClient.GetAsync(host1, new CancellationToken());
        if (response.StatusCode != HttpStatusCode.OK)
        {
            Assert.Fail($"{host1} с заголовком important = {importantDataValue} отработала некорректно, дальнейшие действия бессмысленны!");
        }
        var secretValue = response.Headers.GetValues("Secret").FirstOrDefault();
        return secretValue;
    }
}