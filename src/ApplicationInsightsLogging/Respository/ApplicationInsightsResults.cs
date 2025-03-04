using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInsightsLogging.Respository
{
    public class ApplicationInsightsResults
    {
        public ApplicationInsightsTable[] Tables { get; set; }
        
    }

    public class ApplicationInsightsTable
    {
        public string Name { get; set; }
        public ApplicationInsightsColumn[] Columns { get; set; }
        public object[][] Rows { get; set; }
    }

    public class ApplicationInsightsColumn
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
