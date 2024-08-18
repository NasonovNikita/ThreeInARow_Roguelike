using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map.PlotRooms
{
    public abstract class Plot : MonoBehaviour
    {
        [SerializeField] private PlotData plot;

        public List<Action> CurrentActions;

        [NonSerialized] public string CurrentText;

        public abstract List<string> CurrentActionsTexts { get; }
        protected abstract Dictionary<string, Action> AllActions { get; }

        public void Start()
        {
            CurrentText = plot.text.Value;
            OnChanged?.Invoke();
        }

        public event Action OnChanged;

        public void Choose(int index)
        {
            CurrentActions[index]?.Invoke();

            if (index != plot.next.Count) plot = plot.next[index];

            CurrentText = plot.text.Value;
            CurrentActions = plot.actions.Select(action => AllActions[action]).ToList();

            OnChanged?.Invoke();
        }
    }
}