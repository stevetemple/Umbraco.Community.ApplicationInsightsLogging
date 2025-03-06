using ApplicationInsightsLogging.Repository;
using ApplicationInsightsLogging.Settings;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Api.Management.OpenApi;
using Umbraco.Cms.Api.Common.OpenApi;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace ApplicationInsightsLogging.Composers
{
    public class ApplicationInsightsLoggingApiComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddUnique<ILogViewerRepository, ApplicationInsightsLogViewerRepository>();
            builder.Services
                .AddOptions<ApplicationInsightsSettings>()
                .BindConfiguration("ApplicationInsightsLogging");
        }
    }
}
