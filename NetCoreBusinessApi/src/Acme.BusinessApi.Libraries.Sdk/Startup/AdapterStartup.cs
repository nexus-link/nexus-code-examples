using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Link.Authentication.AspNet.Sdk.Handlers;
using Nexus.Link.Libraries.Core.Application;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Logging;
using Nexus.Link.Libraries.Core.Platform.Authentication;
using Nexus.Link.Libraries.Core.Threads;
using Nexus.Link.Libraries.Web.Logging.Stackify;
using Nexus.Link.Services.Contracts.Capabilities.Integration;
using Nexus.Link.Services.Implementations.Startup;
using Acme.BusinessApi.Libraries.Sdk.Capabilities.Integration;

namespace Acme.BusinessApi.Libraries.Sdk.Startup
{
    /// <summary>
    /// Helper class for the different steps in the Startup.cs file.
    /// </summary>
    public abstract class AdapterStartup : NexusAdapterStartup
    {
        /// <summary>
        /// Use this constructor in the Startup constructor.
        /// </summary>
        /// <param name="configuration">Take this from the Startup constructor.</param>
        protected AdapterStartup(IConfiguration configuration) : base(configuration)
        {
        }

        /// <inheritdoc />
        protected override ISyncLogger GetSynchronousFastLogger()
        {
            var stackifyKey = FulcrumApplication.AppSettings.GetString("Stackify.Key", true);
            FulcrumAssert.IsNotNullOrWhiteSpace(stackifyKey);
            return new StackifyLogger(stackifyKey).AsQueuedSyncLogger().AsBatchLogger();
        }

        /// <inheritdoc />
        protected override void DependencyInjectBusinessApiServices(IServiceCollection services)
        {
            // Inject the Business API SDK
            services.AddScoped<IIntegrationCapability>(provider =>
                ValidateDependencyInjection(provider, p=> CreateIntegrationCapability()));
        }

        /// <inheritdoc />
        protected override void ConfigureAppMiddleware(IApplicationBuilder app, IHostingEnvironment env)
        {
            base.ConfigureAppMiddleware(app, env);
            // Verify tokens with our public key
            var rsaPublicKey = ThreadHelper.CallAsyncFromSync(CreateIntegrationCapability().Authentication.PublicKeyService
                .GetPublicRsaKeyAsXmlAsync);
            app.UseNexusTokenValidationHandler(rsaPublicKey);
        }

        private static IntegrationCapability CreateIntegrationCapability()
        {
            var businessApiUrl = FulcrumApplication.AppSettings.GetString("BusinessApi.Url", true);
            var clientId = FulcrumApplication.AppSettings.GetString("BusinessApi.ClientId", true);
            var clientSecret = FulcrumApplication.AppSettings.GetString("BusinessApi.ClientSecret", true);
            var credentials = new AuthenticationCredentials { ClientId = clientId, ClientSecret = clientSecret };
            return new IntegrationCapability($"{businessApiUrl}/api/Integration/v1", credentials);
        }
    }
}