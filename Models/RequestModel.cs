namespace ProgrammingTest.Models
{
    public class RequestModel
    {
        public string clientSecret { set;  get; }
        public string clientId { get; set; }
        public string script { get; set; }
        public string stdIn { get; set; }
        public string language { get; set; }
        public string versionIndex { get; set; }
    }
}
