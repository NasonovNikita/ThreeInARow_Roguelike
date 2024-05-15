using System.Collections.Generic;
using System.Linq;

namespace DevTools.DeveloperConsole
{
    public struct ModInfo
    {
        public string Description { get; }
        public List<ArgInfo> ArgsInfo { get; }

        public ModInfo(string description, params ArgInfo[] argsInfo)
        {
            Description = description;
            ArgsInfo = argsInfo.ToList();
        }
    }

    public struct ArgInfo
    {
        public string NameAndType { get; }
        public string Description { get; }

        public ArgInfo(string nameAndType, string description)
        {
            NameAndType = nameAndType;
            Description = description;
        }
    }
}