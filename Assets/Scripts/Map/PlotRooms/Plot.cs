using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Map.PlotRooms
{
    public abstract class Plot : MonoBehaviour
    {
        [SerializeField] private PlotData plot;

        [NonSerialized] public string currentText;

        public List<Action> currentActions;

        public abstract List<string> CurrentActionsTexts { get; }
        protected abstract Dictionary<string, Action> AllActions { get; }

        public event Action OnChanged;

        public void Start()
        {
            currentText = plot.text.Value;
            OnChanged?.Invoke();
        }

        public void Choose(int index)
        {
            currentActions[index]?.Invoke();

            if (index != plot.next.Count)
            {
                plot = plot.next[index];
            }

            currentText = plot.text.Value;
            currentActions = plot.actions.Select(action => AllActions[action]).ToList();
            
            OnChanged?.Invoke();
        }
        
        
        private void Quit() 
        {
            GameManager.instance.EnterMap();
        }
    }
}