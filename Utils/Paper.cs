using System;

using System.Net;
using Newtonsoft.Json;

namespace MinecraftServerSoftware.Utils
{
    public class Paper
    {
        public static int GetLatestBuild(string MinecraftVersion)
        {
            WebClient wc = new WebClient();
            PaperJSON paper = JsonConvert.DeserializeObject<PaperJSON>(wc.DownloadString("https://papermc.io/api/v2/projects/paper/versions/" + MinecraftVersion));
            
            int largest = paper.builds[0];
            foreach (int item in paper.builds)
            {
                if (item > largest) { largest = item; }
            }

            return largest;
        }

        public static bool DoesVersionExist(string MinecraftVersion)
        {
            WebClient wc = new WebClient();

            try
            {
                if (wc.DownloadString("https://papermc.io/api/v2/projects/paper/versions/" + MinecraftVersion) ==
                    "{\"error\":\"no such version\"}") { return false; }
                else { return true; }
            }
            catch
            {
                return false;
            }
        }
    }
    public class PaperJSON
    {
        public string? project_id { get; set; }
        public string? project_name { get; set; }
        public string? version { get; set; }
        public int[]? builds { get; set; }
    }
}