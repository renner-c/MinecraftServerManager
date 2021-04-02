using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinecraftServerSoftware.Utils
{
    class CommandExecutor
    {
        public static void ExecuteCommands(List<CommandOrganizer.Operation> commands)
        {
            foreach (CommandOrganizer.Operation command in commands)
            {
                switch (command)
                {
                    case CommandOrganizer.Operation.Create:
                        try
                        {
                            OperationManager.CreateServer(Program.arguments[1]);
                        }
                        catch (Exception ex)
                        {
                            Screen.PrintLn(ex.ToString(), ConsoleColor.Red);
                        }
                        break;
                    case CommandOrganizer.Operation.Delete:
                        try
                        {
                            OperationManager.DeleteServer(Program.arguments[1]);
                        }
                        catch (Exception ex)
                        {
                            Screen.PrintLn(ex.ToString(), ConsoleColor.Red);
                        }
                        break;
                    case CommandOrganizer.Operation.Start:
                        if (Servers.ServerExists(Program.arguments[1]) && Servers.ServerRunning() == false)
                        {
                            Screen.PrintLn("\nThe server will start in 5 seconds, type 'stop' into the console to shut the server down", ConsoleColor.Green);
                            Thread.Sleep(5000);
                            OperationManager.StartServer(Program.arguments[1]);
                        }
                        else if (Servers.ServerRunning())
                        {
                            Screen.PrintLn("\nThis server is already running", ConsoleColor.Green);
                            Environment.Exit(0);
                        }
                        else { Screen.PrintLn("\nServer cannot be found, did you spell the name correctly?", ConsoleColor.Green); }
                        break;
                    case CommandOrganizer.Operation.CheckVersion:
                        break;
                    case CommandOrganizer.Operation.WipeWorld:
                        break;
                }
            }
        }
    }
}
