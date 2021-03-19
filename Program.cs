using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerSoftware
{
    class Program
    {
        public static string[] arguments;
        public static string programdirectory;
        
        public static void Main(string[] args)
        {
            arguments = args;
            programdirectory = Environment.CurrentDirectory;
            if (args.Length < 1) { Utils.Screen.PrintLn("Please include a command", ConsoleColor.Red); }
            else { Utils.CommandExecutor.ExecuteCommands(Utils.CommandOrganizer.ParseCommand(args)); }
        }
    }
}
