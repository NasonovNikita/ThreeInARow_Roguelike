namespace DevTools.DeveloperConsole
{
    public struct CommandInfo
    {
        public string Description { get; }

        public string UseGuide { get; }

        public CommandInfo(string description, string useGuide)
        {
            Description = description;
            UseGuide = useGuide;
        }
    }
}