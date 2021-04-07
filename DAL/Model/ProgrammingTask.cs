namespace ProgrammingTest.DAL.Model
{
    public class ProgrammingTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InputParams { get; set; }
        public string ExpectedOutput { get; set; }
    }
}
