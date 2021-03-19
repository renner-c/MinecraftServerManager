using System;
using System.Collections.Generic;

namespace MinecraftServerSoftware.Utils
{
    internal class CommandOrganizer
    {
        public enum Operation
        {
            Create,
            Delete,
            Start,
            CheckVersion,
            WipeWorld
        }

        public static List<Operation> ParseCommand(string[] args)
        {
            List<Operation> operationList = new List<Operation>();

            if (args[0].ToCharArray()[0] == '-' && args[0].ToCharArray()[1] == '-')
            {
                foreach (string arg in args)
                {
                    switch (arg)
                    {
                        case "create":
                            operationList.Add(Operation.Create);
                            break;
                        case "delete":
                            operationList.Add(Operation.Delete);
                            break;
                        case "start":
                            operationList.Add(Operation.Start);
                            break;
                        case "checkversion":
                            operationList.Add(Operation.CheckVersion);
                            break;
                        case "wipeworld":
                            operationList.Add(Operation.WipeWorld);
                            break;
                        default:
                            break;
                    }
                }
            }
            if (args[0].ToCharArray()[0] == '-' && args[0].ToCharArray()[1] != '-')
            {
                foreach (string arg in args)
                {
                    foreach (char character in arg.ToCharArray())
                    {
                        switch (character)
                        {
                            case 'c':
                                operationList.Add(Operation.Create);
                                break;
                            case 'd':
                                operationList.Add(Operation.Delete);
                                break;
                            case 's':
                                operationList.Add(Operation.Start);
                                break;
                            case 'v':
                                operationList.Add(Operation.CheckVersion);
                                break;
                            case 'w':
                                operationList.Add(Operation.WipeWorld);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                
            }

            return operationList;
        }
    }
}