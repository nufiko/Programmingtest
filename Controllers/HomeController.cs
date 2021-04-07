using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingTest.DAL;
using ProgrammingTest.DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProgrammingTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        DataContext context;

        public HomeController(DataContext dbContext) : base()
        {
            context = dbContext;
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<object>> GetTop3(int taskId)
        {
            var bestOf = context.Submissions.Include(sub => sub.ProgrammingTask)
                .Include(sub =>sub.ProgrammingLanguage).Where(s => s.ProgrammingTask.Id == taskId)
                .OrderByDescending(s => s.Score).Take(3)
                //.Select(sub => new { name = sub.UserName, result = sub.Score, language = sub.ProgrammingLanguage.DisplayName})
                .ToList();

            return bestOf;
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<ProgrammingLanguage>> GetLanguages()
        {
            var languages = context.ProgrammingLanguages.ToList();

            return languages;
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<ProgrammingTask>> GetTasks()
        {
            var tasks = context.ProgrammingTasks.ToList();

            return tasks;
        }

        [HttpGet("[action]")]
        public ActionResult<ProgrammingTask> GetTask(int id)
        {
            var task = context.ProgrammingTasks.Find(id);

            return task;
        }
    }
}
