using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                        break;
                    case CommandOrganizer.Operation.Start:
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
