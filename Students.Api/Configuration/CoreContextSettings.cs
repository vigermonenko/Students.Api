using Microsoft.Extensions.Options;
using Students.Infrastructure;
using Students.Infrastructure.Entities;


namespace Students.Api.Configuration
{
    public class CoreContextSettings : ICoreContextSettings
    {
        public string ConnectionString { get; }


        public CoreContextSettings(IOptions<Settings.Infrastructure> infrastructureOptions)
        {
            ConnectionString = infrastructureOptions.Value.ConnectionString;
        }
    }
}