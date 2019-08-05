using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Link.Libraries.Core.Application;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Logging;
using Nexus.Link.Libraries.Web.Logging.Stackify;
using Nexus.Link.Libraries.Web.Pipe.Outbound;
using Nexus.Link.Services.Contracts.Capabilities.Integration.Authentication;
using Nexus.Link.Services.Contracts.Capabilities.Integration.BusinessEvents;
using Nexus.Link.Services.Implementations.BusinessApi.Capabilities.Integration.Authentication;
using Nexus.Link.Services.Implementations.BusinessApi.Capabilities.Integration.BusinessEvents;
using Nexus.Link.Services.Implementations.Startup;
using Acme.BusinessApi.Libraries.Contracts.Capabilities.Economy.Invoicing;
using Acme.BusinessApi.Service.RestClients.Capabilities.EconomyAdapter;

namespace Acme.BusinessApi.Service
{
    internal class Startup : NexusBusinessApiStartup
    {

        public Startup(IConfiguration configuration) : base(configuration)
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
        protected override void DependencyInjectServices(IServiceCollection services)
        {
            base.DependencyInjectServices(services);

            //
            // Nexus services
            //

            // Authentication
            services.AddScoped<IAuthenticationCapability>(provider =>
                ValidateDependencyInjection(provider,
                    p => new AuthenticationCapability(NexusLinkAuthenticationBaseUrl, GetNexusCredentials())));

            // Business Events
            services.AddScoped<IBusinessEventsCapability>(provider =>
                ValidateDependencyInjection(provider, p =>
                    new BusinessEventsCapability(BusinessEventsBaseUrl, GetNexusCredentials())));

            // Adapter services
            var httpClient = HttpClientFactory.Create(OutboundPipeFactory.CreateDelegatingHandlers());

            // Invoicing
            var invoicingUrl = FulcrumApplication.AppSettings.GetString("InvoiceCapability.Url", true);
            services.AddScoped<IInvoicingCapability>(provider =>
                ValidateDependencyInjection(provider, p =>
                    new InvoicingCapability($"{invoicingUrl}", httpClient, GetLocalCredentials())));
        }
    }
}
