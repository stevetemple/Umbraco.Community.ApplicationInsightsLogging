using ApplicationInsightsLogging.Settings;
using Azure.Identity;
using Azure.Monitor.Query;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Umbraco.Cms.Core.Logging.Viewer;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Logging.Serilog;
using LogLevel = Umbraco.Cms.Core.Logging.LogLevel;

namespace ApplicationInsightsLogging.Repository;

public class ApplicationInsightsLogViewerRepository : ILogViewerRepository
{
    private readonly UmbracoFileConfiguration _umbracoFileConfig;
    private readonly ApplicationInsightsSettings _options;
    
    public ApplicationInsightsLogViewerRepository(IOptions<ApplicationInsightsSettings> options, UmbracoFileConfiguration umbracoFileConfig)
    {
        _umbracoFileConfig = umbracoFileConfig;
        _options = options.Value;
    }

    /// <inheritdoc />
    public IEnumerable<ILogEntry> GetLogs(LogTimePeriod logTimePeriod, string? filterExpression = null)
    {
        return GetRemoteLogs(logTimePeriod, filterExpression).Result;
    }

    /// <inheritdoc />
    public LogLevelCounts GetLogCount(LogTimePeriod logTimePeriod)
    {
        var logs = GetRemoteLogs(logTimePeriod).Result;
        var groups = logs.GroupBy(x => x.Level);

        return new LogLevelCounts
        {
            Debug = groups.Count(x => x.Key == LogLevel.Debug),
            Information = groups.Count(x => x.Key == LogLevel.Information),
            Warning = groups.Count(x => x.Key == LogLevel.Warning),
            Error = groups.Count(x => x.Key == LogLevel.Error),
            Fatal = groups.Count(x => x.Key == LogLevel.Fatal)
        };
    }

    /// <inheritdoc />
    public LogTemplate[] GetMessageTemplates(LogTimePeriod logTimePeriod)
    {
        /* TODO - Reimplement
        var messageTemplates = new MessageTemplateFilter();

        GetLogs(logTimePeriod, messageTemplates);

        return messageTemplates.Counts
            .Select(x => new LogTemplate { MessageTemplate = x.Key, Count = x.Value })
            .OrderByDescending(x => x.Count).ToArray();
        */
        return Enumerable.Empty<LogTemplate>().ToArray();
    }

    /// <inheritdoc />
    public LogLevel GetGlobalMinLogLevel() // TODO : Application Insights Sink Config
    {
        LogEventLevel logLevel = GetGlobalLogLevelEventMinLevel();

        return Enum.Parse<LogLevel>(logLevel.ToString());
    }

    public LogLevel RestrictedToMinimumLevel()
    {
        LogEventLevel minLevel = _umbracoFileConfig.RestrictedToMinimumLevel;
        return Enum.Parse<LogLevel>(minLevel.ToString());
    }

    private LogEventLevel GetGlobalLogLevelEventMinLevel() =>
        Enum.GetValues(typeof(LogEventLevel))
            .Cast<LogEventLevel>()
            .Where(Log.IsEnabled)
            .DefaultIfEmpty(LogEventLevel.Information)
            .Min();

    private async Task<IEnumerable<ILogEntry>> GetRemoteLogs(LogTimePeriod logTimePeriod, string? query = null)
    {
        // TODO - reimplement logFilter

        var client = new LogsQueryClient(new DefaultAzureCredential()); //new ClientSecretCredential(_options.TenantId, _options.ClientId, _options.ClientSecret)); TODO : This isn't getting correct permission
        var result = await client.QueryWorkspaceAsync(_options.WorkspaceId, "AppTraces",
            new QueryTimeRange(logTimePeriod.StartTime, logTimePeriod.EndTime));

        var table = result.Value.Table;


        return table.Rows.Select(x => new LogEntry
        {
            RenderedMessage = x.GetString("Message"),
            Level = (LogLevel)x.GetInt32("SeverityLevel"),
            Timestamp = x.GetDateTimeOffset("TimeGenerated").GetValueOrDefault()
        });
        

    }
    

    
}
