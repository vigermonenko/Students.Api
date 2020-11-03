namespace Students.Api.Settings
{
    public class AppSettings
    {
        public Infrastructure Infrastructure { get; set; }
    }

    public class Infrastructure
    {
        public string ConnectionString { get; set; }
    }
}