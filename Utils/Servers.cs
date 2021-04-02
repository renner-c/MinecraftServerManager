using System;
using System.IO;
using System.Diagnostics;

namespace MinecraftServerSoftware.Utils
{
    public class Servers
    {
        public static bool ServerExists(string servername)
        {
            if (Directory.Exists(@".\server\" + servername)) { return true; }
            else { return false; }
        }

        public static bool ServerRunning()
        {
            bool javaRunning = false;
            bool cmdRunning = false;
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.Contains("java"))
                {
                    javaRunning = true;
                }
                if (process.ProcessName.Contains("cmd"))
                {
                    cmdRunning = true;
                }
            }

            if (cmdRunning == true && javaRunning == true) { return true; }
            else { return false; }
        }
    }
}