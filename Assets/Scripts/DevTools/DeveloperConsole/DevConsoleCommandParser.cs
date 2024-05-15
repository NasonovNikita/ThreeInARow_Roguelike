#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using Battle;
using Battle.Modifiers;
using Battle.Modifiers.Statuses;
using Battle.Units;
using UnityEngine;

namespace DevTools.DeveloperConsole
{
    public class DevConsoleCommandParser : MonoBehaviour
    {
        private const string WrongCommandReply = "No such command";
        private const string ArgumentsRequired = "Arguments required";

        private const string ArgumentsSeparator = " ~";
        
        private delegate string Command(List<string> args);
        private readonly Dictionary<string, Command> commands = new() 
        {
            { "Info", Info },
            { "kill", Kill },
            { "GiveMod", GiveStatusModifier },
            { "GiveStatMod", GiveStatModifier },
            { "ModInfo", ModInfo }
        };
        
        public string Parse(string input)
        {
            if (!commands.TryGetValue(GetCommand(input), out Command command)) return WrongCommandReply;

            string reply = command?.Invoke(GetArgs(input));
            
            return reply;
        }
        

        #region Info
        
        private static readonly Dictionary<string, CommandInfo> CommandInfos = new()
        {
            { 
                "Info", new CommandInfo(
                "Shows info about command", 
                "Info ~{command} optional: ~{description/guide}")
            },
            
            { 
                "Kill", new CommandInfo(
                "Kills specified units",
                $"Kill ~{UnitArgInfo}")
            },
            
            { 
                "GiveStatusMod", new CommandInfo(
                "Gives specified status to unit",
                $"GiveStatusMod ~{UnitArgInfo} " +  
                "~{name (type \"ModInfo ~mods\" for list of mods)} " + 
                "if needed: ~[args (type \"ModInfo ~{modName} ~args\" to get info about mod's arguments)]")
            },
            
            {
                "GiveStatMod", new CommandInfo(
                "Gives specified status to unit",
                $"GiveStatMod ~{UnitArgInfo} " + 
                "~{stat: hp/mana/damage}" + 
                "~{modList (know names for each stat specifically)}" +
                "~{name (type \"ModInfo ~mods\" for list of mods)} " + 
                "if needed: ~[args (type \"ModInfo ~{modName} ~args\" to get info about mod's arguments)]")
                
            },
            
            {
                "ModInfo", new CommandInfo(
                "Shows info about mods and it's arguments",
                "ModInfo ~mods for list of mods\n" +
                "ModInfo ~{mod} optional: ~{description/args")
            }
        };

        private static string Info(string command) => 
            ! CommandInfos.TryGetValue(command, out CommandInfo info)
                ? "No info about command"
                : $"{info.Description}\n{info.UseGuide}";
        
        private static string Info(List<string> args)
        {
            switch (args.Count)
            {
                case 0:
                    return ArgumentsRequired;
                case > 2:
                    return "Too many arguments";
            }

            string command = args[0];
            
            if (args.Count == 1) return Info(command);
            
            if (!CommandInfos.TryGetValue(command, out CommandInfo info)) return "No info about command";
            
            return args[1] switch
            {
                "description" => info.Description,
                "guide" => info.UseGuide,
                _ => "Wrong show type"
            };
        }

        #endregion
        
        
        private static string Kill(List<string> args)
        {
            switch (args.Count)
            {
                case 0:
                    return ArgumentsRequired;
                case > 1:
                    return "Too many arguments";
            }

            if (args[0] == "info") return Info("Kill");
            if (BattleFlowManager.Instance == null)
                return "Battle flow manager doesn't exist. Are you using this command out of battle?";

            if (!TryParseUnits(
                    args[0],
                    out var unitsToKill,
                    out string errorMessage)
                )
                return errorMessage;

            foreach (Unit unit in unitsToKill) unit.Die();

            return $"Killed {unitsToKill.Count} units";
        }
        
        
        #region Modifiers
        
        private static readonly Dictionary<string, List<string>> ListsOfModifiersByType = new()
        {
            {
                "status", new List<string>
                {
                    "burning",
                    "deal",
                    "fury",
                    "immortality",
                    "irritation",
                    "sharp",
                    "stun",
                    "vampirism",
                    "passiveBomb",
                    "randomIgnition"
                }
            },
            {
                "stat", new List<string>
                {
                    "shield",
                    "damageConstMod",
                    "damageMoveMod",
                    "healingConstMod",
                    "hpDamageConstMod",
                    "hpDamageMoveMod",
                    "manaWastingConstMod",
                    "manaWastingMoveMod"
                }
            }
        };

        private static readonly ArgInfo SaveArgInfo = new ArgInfo(
            "save (optional, default=false): bool",
            "save mod after battle end");
        private static readonly Dictionary<string, ModInfo> ModArgumentsInfos = new()
        {
            { 
                "burning", new ModInfo(
                new Burning(0).Description,
                new ArgInfo("moves: int", "burning moves"))
            },
            
            { 
                "deal", new ModInfo(
                new Deal(0).Description,
                new ArgInfo("value: int", "damage addition"),
                SaveArgInfo)
            },
            
            { 
                "fury", new ModInfo(
                new Fury(0, 0).Description,
                new ArgInfo("addition: int", "damage addition"),
                new ArgInfo("hpBorder: int", "hp border to pass"),
                SaveArgInfo)
            }
            
            // TODO ModArgumentsInfos
        };
        
        private static readonly Dictionary<string, ModifierByArgumentList> ModCreatorsByString = new()
        {
            {
                "burning", (List<string> args, out Modifier modifier, out string errorMessage) => 
                    TryCreateMod(args, 
                        new Dictionary<int, ModifierCreator>() 
                        {
                            {1, creatorArgs => new Burning(int.Parse(creatorArgs[0]))}
                        },
                        out modifier,
                        out errorMessage)
            },

            {
                "deal", (List<string> args, out Modifier modifier, out string errorMessage) => 
                    TryCreateMod(args,
                        new Dictionary<int, ModifierCreator>()
                        {
                            {1, creatorArgs => new Deal(int.Parse(creatorArgs[0]))},
                            {2, creatorArgs => 
                                new Deal(int.Parse(creatorArgs[0]), bool.Parse(creatorArgs[1]))}
                        },
                        out modifier,
                        out errorMessage)
            },

            {
                "fury", (List<string> args, out Modifier modifier, out string errorMessage) => TryCreateMod(
                    args,
                    new Dictionary<int, ModifierCreator>() 
                    {
                        { 2, list => new Fury(int.Parse(list[0]), int.Parse(list[1]))},
                        { 3, list => 
                            new Fury(int.Parse(list[0]), int.Parse(list[1]), bool.Parse(list[2]))}
                    },
                    out modifier,
                    out errorMessage)
            }
        };
        private delegate bool ModifierByArgumentList(List<string> args, out Modifier modifier, out string errorMessage);
        
        private static bool TryCreateMod(List<string> args, 
            IReadOnlyDictionary<int, ModifierCreator> creators,
            out Modifier modifier, out string errorMessage)
        {
            modifier = null;
            errorMessage = "";

            if (!creators.ContainsKey(args.Count))
            {
                errorMessage = "Wrong arguments count (mod)"; return false;
            }
            
            try
            {
                modifier = creators[args.Count].Invoke(args);
            }
            catch
            {
                errorMessage = "Argument type error (mod)"; return false;
            }

            return true;
        }
        private delegate Modifier ModifierCreator(List<string> args);
        
        private static string ModInfo(List<string> args)
        {
            return "TODO"; // TODO ModInfo
        }

        private static string GiveStatusModifier(List<string> args)
        {
            switch (args.Count)
            {
                case 0:
                    return ArgumentsRequired;
                case < 2:
                    return "Not enough arguments";
                case > 3:
                    return "Too many arguments";
            }

            if (args[0] == "info") return Info("GiveStatusModifier");

            if (!TryParseUnits(args[0], out var units, out string errorMessage)) return errorMessage;

            string name = args[1];

            if (!ListsOfModifiersByType["status"].Contains(name)) return "No such status mod";

            if (!ModCreatorsByString[name].Invoke(args, out Modifier modifier, out errorMessage)) return errorMessage;

            foreach (Unit unit in units)
            {
                ModCreatorsByString[name].Invoke(args, out modifier, out errorMessage);

                unit.Statuses.Add((Status)modifier);
            }

            return $"Added status {modifier} to {units.Count} unit(s)";
        }

        private static string GiveStatModifier(List<string> args)
        {
            return "TODO"; // TODO GiveStatModifier
        }

        #endregion
        
        
        private const string UnitArgInfo = "{unit: all/player/enemies/enemy [indexes]}";
        private static bool TryParseUnits(string arg, out List<Unit> units, out string errorMessage)
        {
            var args = arg.Split(" ").ToList();
            bool res = TryParseUnits(args, out var units1, out var errorMessage1);
            units = units1;
            errorMessage = errorMessage1;
            return res;
        }
        private static bool TryParseUnits(List<string> args, out List<Unit> units, out string errorMessage)
        {
            units = null;
            errorMessage = null;
            
            switch (args[0])
            {
                case "everyone":
                case "all":
                    units = new List<Unit>(BattleFlowManager.Instance.EnemiesWithoutNulls)
                        { Player.Instance };
                    break;
                case "player":
                    units = new List<Unit>() { Player.Instance };
                    break;
                case "enemies":
                    units = new List<Unit>(BattleFlowManager.Instance.EnemiesWithoutNulls); 
                    break;
                case "enemy":
                    if (args.Count == 1)
                    {
                        errorMessage = "Indexes of enemies required"; return false;
                    }
                    
                    units = new List<Unit>();
                    foreach (string index in args.GetRange(1, args.Count - 1))
                    {
                        if (int.TryParse(index, out int i))
                        {
                            if (!(i >= 0 && i < BattleFlowManager.Instance.enemiesWithNulls.Count))
                            {
                                errorMessage = "Index is out of range (enemies)"; return false;
                            }
                            
                            units.Add(BattleFlowManager.Instance.enemiesWithNulls[i]);
                        }
                        else
                        {
                            errorMessage = "Wrong enemy index format"; return false;
                        }
                    }
                    break;
                default:
                    errorMessage = "Wrong unit key"; return false;
            }

            units = units.Where(unit => unit != null && !unit.Dead).ToList();

            return true;
        }

        private static string GetCommand(string input) => input.Split(ArgumentsSeparator)[0];

        private static List<string> GetArgs(string input)
        {
            var parts = input.Split(ArgumentsSeparator);
            
            return parts.Length == 1 ? new List<string>() : parts[1..].ToList();
        }
    }
}

#endif