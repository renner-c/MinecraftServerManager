using System;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MinecraftServerSoftware.Utils
{
    public class OperationManager
    {
        public static void CreateServer(string servername)
        {
            Screen.Print("\nWhat Minecraft version would you like the server to be? >> ", ConsoleColor.Green);
            string chosenversion = Console.ReadLine();

            if (Paper.DoesVersionExist(chosenversion) == false)
            {
                Screen.PrintLn("Version is not available", ConsoleColor.Green);
                Environment.Exit(0);
            }
            
            Directory.CreateDirectory("./server/" + servername);

            // downloading paper.jar and recording version
            WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile(
                    "https://papermc.io/api/v2/projects/paper/versions/" + chosenversion + "/builds/" +
                    Paper.GetLatestBuild(chosenversion) + "/downloads/paper-" + chosenversion + "-" +
                    Paper.GetLatestBuild(chosenversion) + ".jar", "./server/" + servername + "/paper.jar");
            }
            catch (Exception ex)
            {
                Screen.PrintLn(ex.ToString(), ConsoleColor.Red);
            }
            File.WriteAllText("./server/" + servername + "/serverversion.ver", chosenversion + "\n" + Paper.GetLatestBuild(chosenversion).ToString());
            
            // creating start.bat
            Screen.Print("How much dedicated RAM (in gigs) would you like the server to have? (default is 2G) >> ", ConsoleColor.Green);
            string dedicatedram = Console.ReadLine();
            if (dedicatedram == "")
            {
                dedicatedram = "2";
            }
            File.WriteAllText("./server/" + servername + "/start.bat", "java -Xmx" + dedicatedram + "G -Xms1024M -jar paper.jar nogui");
            
            //agreeing to EULA
            Screen.Print("Do you agree to the EULA? Type `agree` to confirm you agree to the EULA >> ", ConsoleColor.Green);
            string input = Console.ReadLine();
            if (input.ToLower() == "agree") { SilentStartServer(servername); }
            else 
            {
                Screen.PrintLn("You have not agreed to the EULA, thus ending the process" , ConsoleColor.Red);
                Directory.Delete("./server/" + servername, true);
                Environment.Exit(0);
            }

            string eula = File.ReadAllText("./server/" + servername + "/eula.txt");
            eula = eula.Replace("false", "true");
            File.WriteAllText("./server/" + servername + "/eula.txt", eula);

            // starting or saving server
            Screen.PrintLn("Would you like to start the server? (Y/N) >> ", ConsoleColor.Green);
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.KeyChar == 'y')
            {
                Screen.PrintLn("Your server will start in 5 seconds. To stop the server, type `stop` into the console", ConsoleColor.Green);
                Thread.Sleep(5000);
                StartServer(servername);
            }
            else { Screen.PrintLn("Use the start command to start your server at any time", ConsoleColor.Green); }

            //closing WebClient
            wc.Dispose();
        }
        
        public static void DeleteServer(string servername)
        {
            if (Directory.Exists(@".\server\" + servername))
            {
                Screen.PrintLn("\nAre you sure you would like to delete '" + servername + "'? (Y/N)", ConsoleColor.Green);
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar != 'y')
                {
                    Screen.PrintLn("Cancelling", ConsoleColor.Green);
                    Environment.Exit(0);
                }
            }
            else
            {
                Screen.PrintLn("Server does not exist!", ConsoleColor.Red);
                Environment.Exit(0);
            }

            Screen.PrintLn("Attempting to delete '" + servername + "'...", ConsoleColor.Green);
            if (Servers.ServerRunning())
            {
                Screen.PrintLn("You cannot delete the server while it is running, to stop the server type 'stop' into the server console", ConsoleColor.Green); 
                Environment.Exit(0);
            }
            Directory.Delete(@".\server\" + servername, true);
            Screen.PrintLn("Successfully deleted '" + servername + "'", ConsoleColor.Green);
        }

        public static void SilentStartServer(string servername)
        {
            Process proc = null;
            Environment.CurrentDirectory = Program.programdirectory + @"\server\" + servername;
            string _batDir = string.Format(Program.programdirectory + @"\server\" + servername);
            proc = new Process();
            proc.StartInfo.WorkingDirectory = _batDir;
            proc.StartInfo.FileName = "start.bat";
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            proc.WaitForExit();
            proc.Close();
            Environment.CurrentDirectory = Program.programdirectory;
        }
        
        public static void StartServer(string servername)
        {
            var p = new Process();  
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.FileName = "start.bat";
            Environment.CurrentDirectory = Program.programdirectory + @"\server\" + servername;
            p.Start();
        }
    }
}