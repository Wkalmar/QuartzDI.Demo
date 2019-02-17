using System;
using System.Net.Http;

namespace QuartzDi.Demo.Extrernal
{
    public interface IDemoService
    {
        void DoTask(string url);
    }

    public class DemoService : IDemoService
    {
        public void DoTask(string url)
        {
            using (var httpClient = new HttpClient())
{
                Console.WriteLine($"calling {url}");
                httpClient.GetAsync(url);
            }
        }
    }
}
