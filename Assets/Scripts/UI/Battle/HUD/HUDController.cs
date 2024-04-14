using System.Collections.Generic;
using UnityEngine;

namespace UI.Battle.HUD
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private HUDSpawner hudSpawner;
        [SerializeField] private List<StatHUDData> statHUDData;

        public void Awake()
        {
            foreach (var hudData in statHUDData)
                hudData.stat.OnValueChanged += valueChange => SpawnStatHUD(valueChange, hudData);
        }

        private void SpawnStatHUD(int statValueChange, StatHUDData hudData)
        {
            hudSpawner.SpawnHUD(statValueChange.ToString(),
                hudData.ColorByStatValueChange(statValueChange),
                hudData.HUDMoveDirectionByStatValueChange(statValueChange));
        }
    }
}