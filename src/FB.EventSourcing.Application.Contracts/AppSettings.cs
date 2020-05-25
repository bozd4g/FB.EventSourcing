namespace FB.EventSourcing.Application.Contracts
{
    public class AppSettings
    {
        public SwaggerSettings Swagger { get; set; }
    }

    public class SwaggerSettings
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
    }
}