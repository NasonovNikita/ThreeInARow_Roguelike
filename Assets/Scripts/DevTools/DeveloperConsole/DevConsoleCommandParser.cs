#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Battle;
using Battle.Items;
using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;
using Battle.Units.Statuses;
using Map.Nodes.Managers;
using UnityEngine;

namespace DevTools.DeveloperConsole
{
    public class DevConsoleCommandParser : MonoBehaviour
    {
        private const string WrongCommandReply = "No such command";
        private const string ArgumentsRequired = "Arguments required";

        private const string ArgumentsSeparator = " ~";

        private readonly Dictionary<string, Command> _commands = new()
        {
            { "Info", Info },
            { "Kill", Kill },
            { "GiveStatusMod", GiveStatusModifier },
            { "GiveStatMod", GiveStatModifier },
            { "ChangeStatValue", ChangeStat },
            { "UnlockMap", UnlockMap },
            { "LockMap", LockMap },
            { "GiveItem", GiveItem }
        };

        /// <summary>
        ///     Parses user's input and converts it to executable
        ///     <see cref="Func{String}">command</see> that returns an execution reply (string).<br/>
        ///     Also return a reply itself.
        /// </summary>
        /// <param name="input">User's input</param>
        /// <param name="reply">Parser's reply that can be logged</param>
        /// <returns>Func with string reply</returns>
        public Func<string> Parse(string input, out string reply)
        {
            reply = "SUCCESSFULLY PARSED COMMAND";
            if (_commands.TryGetValue(GetCommand(input), out var command))
                return ResultingFunc;

            reply = WrongCommandReply;
            return null;

            string ResultingFunc() => command.Invoke(GetArgs(input));
        }

        private static string GetCommand(string input) =>
            input.Split(ArgumentsSeparator)[0];

        private static List<string> GetArgs(string input)
        {
            var parts = input.Split(ArgumentsSeparator);

            return parts.Length == 1 ? new List<string>() : parts[1..].ToList();
        }

        /// <summary>
        ///     Applies some action (if possible) and return reply (errors/warnings/success/etc.).
        /// </summary>
        private delegate string Command(List<string> args);

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
                    "Gives specified status to unit\n",
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
            !CommandInfos.TryGetValue(command, out var info)
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

            var command = args[0];

            if (args.Count == 1) return Info(command);

            if (!CommandInfos.TryGetValue(command, out var info))
                return "No info about command";

            return args[1] switch
            {
                "description" => info.Description,
                "guide" => info.UseGuide,
                _ => "Wrong show type"
            };
        }

        #endregion

        #region Battle

        private static string ChangeStat(List<string> args)
        {
            switch (args.Count)
            {
                case 0:
                    return ArgumentsRequired;
                case < 3:
                    return "Not enough arguments";
                case > 3:
                    return "Too many arguments";
            }

            if (!TryParseUnits(args[0], out var units, out var errorMessage))
                return errorMessage;

            if (!int.TryParse(args[2], out var val)) return "Wrong value argument";


            var count = 0;

            foreach (var unit in units)
            {
                Stat stat;

                switch (args[1])
                {
                    case "hp":
                        stat = unit.hp;
                        break;
                    case "mana":
                        stat = unit.mana;
                        break;
                    case "damage":
                        stat = unit.damage;
                        break;
                    default:
                        return "wrong stat";
                }

                stat.ChangeValue(val);
                count++;
            }

            return $"Changed {count} unit(s)' {args[1]} by {val}";
        }

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
                return
                    "Battle flow manager doesn't exist. Are you using this command out of battle?";

            if (!TryParseUnits(
                    args[0],
                    out var unitsToKill,
                    out var errorMessage)
               )
                return errorMessage;

            foreach (var unit in unitsToKill) unit.Die();

            return $"Killed {unitsToKill.Count} units";
        }


        #region Modifiers

        private static readonly Dictionary<string, List<string>> ListsOfModifiersByType =
            new()
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

        private static bool TryCreateStatusMod(IReadOnlyList<string> args, string modName,
            out Modifier modifier, out string errorMessage) =>
            TryCreateMod(args, modName, "Battle.Units.Statuses", out modifier,
                out errorMessage);

        private static bool TryCreateStatMod(IReadOnlyList<string> args, string modName,
            out Modifier modifier, out string errorMessage) =>
            TryCreateMod(args, modName, "Battle.Units.StatModifiers", out modifier,
                out errorMessage);

        private static bool TryCreateMod(IReadOnlyCollection<string> args,
            string modName, string assembly,
            out Modifier modifier, out string errorMessage)
        {
            modifier = null;
            errorMessage = "";


            try
            {
                var modType = Type.GetType($"{assembly}.{modName}");
                if (modType != null)
                {
                    var constructor =
                        modType.GetConstructors(BindingFlags.Instance |
                                                BindingFlags.Public)
                            .First(v => v.GetParameters().Length != 0);

                    if (args.Count > constructor.GetParameters().Length)
                    {
                        errorMessage = "Too many arguments";
                        return false;
                    }

                    modifier = (Modifier)constructor.Invoke(args.Select((t, i) =>
                            Convert.ChangeType(t,
                                constructor.GetParameters()[i].ParameterType))
                        .ToArray());
                }
                else
                {
                    errorMessage = "No type found";
                    return false;
                }
            }
            catch (Exception e)
            {
                errorMessage = $"Unknown exception:\n{e}";
                return false;
            }

            return true;
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

            if (!TryParseUnits(args[0], out var units, out var errorMessage))
                return errorMessage;

            var name = args[1].Split(" ")[0];

            if (!ListsOfModifiersByType["status"].Contains(name))
                return "No such status mod";

            name = char.ToUpper(name[0]) + name[1..];

            if (!TryCreateStatusMod(args[1].Split(" ")[1..], name, out var modifier,
                    out errorMessage))
                return errorMessage;

            foreach (var unit in units)
            {
                TryCreateStatusMod(args[1].Split(" ")[1..], name, out modifier,
                    out errorMessage);

                unit.Statuses.Add((Status)modifier);
            }

            return $"Added status {modifier} to {units.Count} unit(s)";
        }

        private static string GiveStatModifier(List<string> args)
        {
            switch (args.Count)
            {
                case 0:
                    return ArgumentsRequired;
                case < 3:
                    return "Not enough arguments";
                case > 3:
                    return "Too many arguments";
            }

            if (args[0] == "info") return Info("GiveStatMod");

            if (!TryParseUnits(args[0], out var units, out var errorMessage))
                return errorMessage;

            var stat = args[1].Split(" ")[0];
            var modList = args[1].Split(" ")[1];

            var mod = args[2];

            var name = mod.Split(" ")[0];
            var modArgs = mod.Split(" ")[1..];


            if (!ListsOfModifiersByType["stat"].Contains(name)) return "No such stat mod";

            name = char.ToUpper(name[0]) + name[1..];

            if (!TryCreateStatMod(modArgs, name, out var modifier, out errorMessage))
                return errorMessage;

            var count = 0;

            const string noSuchList = "No such modifier list";
            foreach (var unit in units)
            {
                ModifierList modifierList;
                switch (stat)
                {
                    case "hp":
                        switch (modList)
                        {
                            case "onTakingDamage":
                                modifierList = unit.hp.onTakingDamageMods;
                                break;
                            case "onHealing":
                                modifierList = unit.hp.onHealingMods;
                                break;
                            default:
                                return noSuchList;
                        }

                        break;
                    case "mana":
                        switch (modList)
                        {
                            case "wasting":
                                modifierList = unit.mana.wastingMods;
                                break;
                            case "refilling":
                                modifierList = unit.mana.refillingMods;
                                break;
                            default:
                                return noSuchList;
                        }

                        break;
                    case "damage":
                        switch (modList)
                        {
                            case "mods":
                                modifierList = unit.damage.mods;
                                break;
                            default:
                                return noSuchList;
                        }

                        break;
                    default:
                        return "No such stat";
                }

                TryCreateStatMod(modArgs, name, out modifier, out errorMessage);
                modifierList.Add(modifier);

                count++;
            }


            return $"Added mod to {count} unit(s)";
        }

        #endregion


        private const string UnitArgInfo = "{unit: all/player/enemies/enemy [indexes]}";

        private static bool TryParseUnits(string arg, out List<Unit> units,
            out string errorMessage)
        {
            var args = arg.Split(" ").ToList();
            var res = TryParseUnits(args, out var units1, out var errorMessage1);
            units = units1;
            errorMessage = errorMessage1;
            return res;
        }

        private static bool TryParseUnits(List<string> args, out List<Unit> units,
            out string errorMessage)
        {
            units = null;
            errorMessage = null;

            switch (args[0])
            {
                case "everyone":
                case "all":
                    units = new List<Unit>(BattleFlowManager.Instance.EnemiesAlive)
                        { Player.Instance };
                    break;
                case "player":
                    units = new List<Unit> { Player.Instance };
                    break;
                case "enemies":
                    units = new List<Unit>(BattleFlowManager.Instance
                        .EnemiesAlive);
                    break;
                case "enemy":
                    if (args.Count == 1)
                    {
                        errorMessage = "Indexes of enemies required";
                        return false;
                    }

                    units = new List<Unit>();
                    foreach (var index in args.GetRange(1, args.Count - 1))
                        if (int.TryParse(index, out var i))
                        {
                            if (!(i >= 0 && i < BattleFlowManager.Instance
                                    .EnemiesWithNulls.Count))
                            {
                                errorMessage = "Index is out of range (enemies)";
                                return false;
                            }

                            units.Add(BattleFlowManager.Instance.EnemiesWithNulls[i]);
                        }
                        else
                        {
                            errorMessage = "Wrong enemy index format";
                            return false;
                        }

                    break;
                default:
                    errorMessage = "Wrong unit key";
                    return false;
            }

            units = units.Where(unit => unit != null && !unit.Dead).ToList();

            return true;
        }

        #endregion

        #region Map

        private static string UnlockMap(List<string> args)
        {
            if (args.Count != 0) return "No arguments required";

            NodeController.Instance.Unlocked = true;

            return "unlocked";
        }

        private static string LockMap(List<string> args)
        {
            if (args.Count != 0) return "No arguments required";

            NodeController.Instance.Unlocked = false;

            return "locked";
        }

        #endregion

        #region OverallActions

        private static string GiveItem(IReadOnlyList<string> args)
        {
            switch (args.Count)
            {
                case 0:
                    return ArgumentsRequired;
                case > 1:
                    return "Too many arguments";
            }

            var name = char.ToUpper(args[0][0]) + args[0][1..];

            if (!TryCreateItem(name, out var item, out var errorMessage))
                return errorMessage;

            item.Get();
            return $"Added item: {item}";
        }


        private static bool TryCreateItem(string name, out Item item,
            out string errorMessage)
        {
            item = null;
            errorMessage = "";

            try
            {
                item = Resources.Load<Item>($"Items/{name}");

                return true;
            }
            catch (Exception e)
            {
                errorMessage = $"Caught exception: {e}";
                return false;
            }
        }

        #endregion
    }
}

#endif