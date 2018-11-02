using System;
using System.Net.Http;
using System.Threading.Tasks;
using Kubeless.Functions;

public class asyncget
{
    public async Task<string> foo(Event k8Event, Context k8Context)
    {
        using (var httpClient = new HttpClient())
        {
            return await httpClient.GetStringAsync("https://google.com");
        }
    }
}
