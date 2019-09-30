using Blazor.Extensions.Storage;
using CandidateEvaluator.Client.Services;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateEvaluator.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UserIdentityService>();
            services.AddStorage();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
