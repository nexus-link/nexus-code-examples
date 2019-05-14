using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Nexus.Link.Libraries.Core.Application;
using Nexus.Link.Libraries.Core.MultiTenant.Model;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Crm.NexusAdapter.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Nexus.Link.Libraries.Web.AspNet.Application.FulcrumApplicationHelper.WebBasicSetup("Crm.NexusAdapter", new Tenant("local", "dev"), RunTimeLevelEnum.Development);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member