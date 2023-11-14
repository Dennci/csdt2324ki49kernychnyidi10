using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Alarm.Models
{
    /// <summary>
    /// A static class that we use to store data about the selected city
    /// </summary>
    public static class MyCityModel
    {
        public static string Name { get; set; }
        public static bool AlertNow { get; set; }
    }
}
