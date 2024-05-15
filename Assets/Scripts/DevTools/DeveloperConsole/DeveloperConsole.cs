#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DevTools.DeveloperConsole
{
    public class DeveloperConsole : MonoBehaviour
    {
        private const string CommandStringToClear = "clear";
        private const string CommandStringToExit = "exit";
        
        [SerializeField] private DevConsoleCommandParser parser;
        [FormerlySerializedAs("textWindow")]
        [SerializeField] private Text text;
        [SerializeField] private InputField input;
        [SerializeField] private Button submitButton;

        [SerializeField] private int maximumLinesCount;

        private List<string> commandsBuffer = new();
        
        public void Submit()
        {
            string inputSting = input.text;
            
            switch (inputSting)
            {
                case CommandStringToClear:
                    ClearConsole();
                    return;
                case CommandStringToExit:
                    Destroy(gameObject);
                    return;
            }
            AddCommandToBuffer(inputSting);
            
            string reply = parser.Parse(inputSting);
            Write(reply);
            
            ClearCommandLine();
        }

        private void Write(string content)
        {
            if (text.text.Count(c => c == '\n') == maximumLinesCount - 1)
            {
                text.text = string.Join("\n", text.text.Split('\n')[1..]);
            }
            text.text = $"{text.text}{content}\n";
        }

        private void ClearConsole()
        {
            commandsBuffer = new List<string>();
            text.text = "";
            ClearCommandLine();
        }
        
        private void ClearCommandLine() => input.text = "";

        private void AddCommandToBuffer(string command)
        {
            if (commandsBuffer.Count == 0 || commandsBuffer[^1] != command) commandsBuffer.Add(command);
        }
    }
}

#endif