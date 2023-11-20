using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Alarm.Models
{
    /// <summary>
    /// A model for retrieving data from ARI
    /// </summary>
    public class AlertModel
    {
        public string Source { get; set; }
        public string Cachedat { get; set; }
        public Dictionary<string, RegionData> States { get; set; }
    }

    public class RegionData
    {
        public bool AlertNow { get; set; }
        public string Changed { get; set; }
    }
}
