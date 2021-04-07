using Microsoft.AspNetCore.Mvc;
using ProgrammingTest.Compilers;
using ProgrammingTest.DAL;
using ProgrammingTest.DAL.Model;
using ProgrammingTest.Models;
using System.Threading.Tasks;

namespace ProgrammingTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompilerApiController : ControllerBase
    {
        IRunner codeRunner;
        DataContext context;

        public CompilerApiController(DataContext context, IRunner codeRunner) : base()
        {
            this.context = context;
            this.codeRunner = codeRunner;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<string>> RunCode([FromBody] TestRun testRun)
        {
            if (string.IsNullOrWhiteSpace(testRun.Code))
                return null;

            var response = await codeRunner.RunCodeAsync(testRun.Code, testRun.Input);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return content;
            }
            return BadRequest(content);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> SubmitSolution([FromBody] FinalCode codeSubmission)
        {
            var task = context.ProgrammingTasks.Find(codeSubmission.TaskId);

            var result = await codeRunner.GetScore(codeSubmission.Code, task);

            var newSubmission = new Submission
            {
                Code = codeSubmission.Code,
                UserName = codeSubmission.Name,
                Score = result,
                ProgrammingTask = task
            };

            context.Submissions.Add(newSubmission);
            context.SaveChanges();

            return Ok(result);
        }
    }
}
