using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LearningASPTest.E2E;

internal class LearningAspWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // builder.UseEnvironment("Testing");

        base.ConfigureWebHost(builder);
    }
}