const i = [
  {
    name: "Application Insights Logging Entrypoint",
    alias: "ApplicationInsightsLogging.Entrypoint",
    type: "backofficeEntryPoint",
    js: () => import("./entrypoint-Bk3jQopD.js")
  }
], n = [
  {
    name: "Application Insights Logging Dashboard",
    alias: "ApplicationInsightsLogging.Dashboard",
    type: "dashboard",
    js: () => import("./dashboard.element-D3WLE0KJ.js"),
    meta: {
      label: "Example Dashboard",
      pathname: "example-dashboard"
    },
    conditions: [
      {
        alias: "Umb.Condition.SectionAlias",
        match: "Umb.Section.Content"
      }
    ]
  }
], a = [
  ...i,
  ...n
];
export {
  a as manifests
};
//# sourceMappingURL=application-insights-logging.js.map
