export const manifests: Array<UmbExtensionManifest> = [
  {
    name: "Application Insights Logging Dashboard",
    alias: "ApplicationInsightsLogging.Dashboard",
    type: 'dashboard',
    js: () => import("./dashboard.element"),
    meta: {
      label: "Example Dashboard",
      pathname: "example-dashboard"
    },
    conditions: [
      {
        alias: 'Umb.Condition.SectionAlias',
        match: 'Umb.Section.Content',
      }
    ],
  }
];
