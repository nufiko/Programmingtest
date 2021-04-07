using ProgrammingTest.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProgrammingTest.Compilers
{
    public interface IRunner
    {
        Task<HttpResponseMessage> RunCodeAsync(string code, string inputParams);
        Task<int> GetScore(string code, ProgrammingTask task);
    }
}
