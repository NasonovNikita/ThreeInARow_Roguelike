using System.Collections.Generic;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace Map.PlotRooms
{
    public class PlotView : MonoBehaviour
    {
        [SerializeField] private Transform buttonsGrid;
        [SerializeField] private Text text;

        [SerializeField] private Plot plot;

        [SerializeField] private Button buttonPrefab;

        private readonly List<Button> _spawnedButtons = new();

        public void Awake()
        {
            plot.OnChanged += ReDraw;
        }

        private void ReDraw()
        {
            text.text = plot.CurrentText;

            SpawnButtons();
        }

        private void SpawnButtons()
        {
            foreach (var btn in _spawnedButtons) Destroy(btn.gameObject);

            for (var i = 0; i < plot.CurrentActions.Count; i++)
            {
                var btn = Instantiate(buttonPrefab, buttonsGrid, false);
                btn.InitButton(plot.CurrentActions[i], plot.CurrentActionsTexts[i]);
                _spawnedButtons.Add(btn);
            }
        }
    }
}