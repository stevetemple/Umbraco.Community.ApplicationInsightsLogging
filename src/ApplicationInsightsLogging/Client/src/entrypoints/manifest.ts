export const manifests: Array<UmbExtensionManifest> = [
  {
    name: "Application Insights Logging Entrypoint",
    alias: "ApplicationInsightsLogging.Entrypoint",
    type: "backofficeEntryPoint",
    js: () => import("./entrypoint"),
  }
];
