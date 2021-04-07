using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingTest.DAL
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                if (context.ProgrammingTasks.Any())
                    return;

                context.ProgrammingTasks.AddRange(
                    new Model.ProgrammingTask
                    {
                        Id = 1,
                        Description = "Create Hello World!",
                        Title = "Hello World!",
                        ExpectedOutput = "Hello World!\n",
                        InputParams = ""
                    },
                    new Model.ProgrammingTask
                    {
                        Id = 2,
                        Description = "Find max value from given params",
                        Title = "Find max",
                        InputParams = "1 4 5 7 2 9 4 3",
                        ExpectedOutput = "9"
                    });

                context.ProgrammingLanguages.AddRange(
                    new Model.ProgrammingLanguage
                    {
                        Id = 1,
                        DisplayName = "C#",
                        Param = "csharp"
                    },
                    new Model.ProgrammingLanguage
                    {
                        Id = 2,
                        DisplayName = "Java",
                        Param = "java"
                    });

                context.Submissions.AddRange(
                    new Model.Submission
                    {
                        Id = 1,
                        ProgrammingLanguage = context.ProgrammingLanguages.Find(1),
                        ProgrammingTask = context.ProgrammingTasks.Find(1),
                        Score = 1241,
                        UserName = "Jan"
                    });

                context.SaveChanges();
            }
        }
    }
}
