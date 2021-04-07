namespace ProgrammingTest.DAL.Model
{
    public class Submission
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Code { get; set; }
        public int Score { get; set; }
        public ProgrammingTask ProgrammingTask { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
    }
}
