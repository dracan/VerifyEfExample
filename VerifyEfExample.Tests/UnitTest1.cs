using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VerifyEfExample.Api.Data;
using VerifyTests;
using VerifyTests.EntityFramework;
using VerifyXunit;
using Xunit;

namespace VerifyEfExample.Tests;

[UsesVerify]
public class UnitTest1
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer("Server=localhost;Database=blah;User Id=sa;Password=localdevpassword#123;");
                    options.EnableRecording();
                });
            });
        }
    }

    [Fact]
    public async Task Test1()
    {
        VerifyEntityFramework.Enable();

        var waf = new CustomWebApplicationFactory<Program>();

        var httpClient = waf.CreateClient();

        EfRecording.StartRecording();
        var response = await httpClient.PostAsync("/blah", null);

        var content = await response.Content.ReadAsStringAsync();

        await Verifier.Verify(content);
    }
}