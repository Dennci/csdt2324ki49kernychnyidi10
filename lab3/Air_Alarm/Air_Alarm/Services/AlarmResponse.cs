using Air_Alarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Air_Alarm.Services
{
    public static class AlarmResponse
    {
        public static AlertModel States { get; set; }

        public static async Task<AlertModel> GetAlarmResponse()
        {
            string url = "https://ubilling.net.ua/aerialalerts/?source=[default]";
            HttpClient httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetFromJsonAsync<AlertModel>(url);
                return response;
            }
            catch (Exception ex)
            {
                return new AlertModel();
            }

        }
    }
}
