using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace BotUI.API {
    public static class apiManeger {


        static string apiBase = @"http://127.0.0.1:5000/API";

        static readonly HttpClient client = new HttpClient();

        public static async Task<HttpResponseMessage> get() {
            return null;
        }

        public static async Task<bool> validateVersion() {
            return true;
            #pragma warning disable CS0162 // Unreachable code detected
            string path = apiBase + @"/Version";
            #pragma warning restore CS0162 // Unreachable code detected

            string pattern = @"\d{1,}\.\d{1,}\.\d{1,}";

            Debug.WriteLine($"API call to {path}");

            HttpResponseMessage res = await client.GetAsync(path);
            res.EnsureSuccessStatusCode();
            string responseBody = await res.Content.ReadAsStringAsync();
            
            
            MatchCollection matches = Regex.Matches(responseBody, pattern);
            Version bot_version = Assembly.LoadFrom("bot.dll").GetName().Version;
            string bot_v = $"{bot_version.Major}.{bot_version.Minor}.{bot_version.Build}";
            Debug.WriteLine($"{bot_v} == {matches[0]}");

            return bot_v == matches[0].ToString();
        }

    }
}
