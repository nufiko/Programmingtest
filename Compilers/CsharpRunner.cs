using Newtonsoft.Json;
using ProgrammingTest.DAL.Model;
using ProgrammingTest.Models;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingTest.Compilers
{
    public class CsharpRunner : IRunner
    {
        const string compilerUrl = @"https://api.jdoodle.com/v1/";
        const string executeEndpoint = compilerUrl + @"execute";

        const string clientId = @"3421422fe0ab31be2e76eaf379477634";
        const string clientSecret = @"e72a490ba7a1c23668510e865bdf55e56196180046676843366bcfddbbda5b0f";

        HttpClient httpClient;
        RequestModel request;

        public CsharpRunner()
        {
            httpClient = new HttpClient();
            request = new RequestModel
            {
                clientId = clientId,
                clientSecret = clientSecret,
                language = "csharp",
                versionIndex = "3"
            };
        }

        public async Task<int> GetScore(string code, ProgrammingTask task)
        {
            var response = await RunCodeAsync(code, task.InputParams);

            if(!response.IsSuccessStatusCode)
            {
                return 0;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var sucessfullResponse = JsonConvert.DeserializeObject<SuccesfullResponse>(responseContent);

            return CalculateScore(sucessfullResponse, task.ExpectedOutput);
        }

        public async Task<HttpResponseMessage> RunCodeAsync(string code, string inputParams)
        {
            request.script = code;
            request.stdIn = inputParams ?? string.Empty;
            var requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(executeEndpoint);
            requestMessage.Method = HttpMethod.Post;
            var content = JsonConvert.SerializeObject(request);
            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(requestMessage);

            return response;
        }

        private int CalculateScore(SuccesfullResponse response, string expectedOutput)
        {
            if(string.CompareOrdinal(response.Output, expectedOutput) != 0)
            {
                return 0;
            }

            var result = 1000;
            result += (1000000 - int.Parse(response.Memory))/1000;
            result += 1000 - (int)Math.Round(1000 * decimal.Parse(response.CpuTime, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture));

            return result;
        }
    }
}
