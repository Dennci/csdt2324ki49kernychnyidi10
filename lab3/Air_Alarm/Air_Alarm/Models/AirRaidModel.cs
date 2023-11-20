using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Alarm.Models
{
    /// <summary>
    /// model that we directly transmit to the arduino
    /// </summary>
    public class AirRaidModel
    {
        public string Region { get; set; }
        public bool AlertNow { get; set; }
    }
}
