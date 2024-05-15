using System.Collections.Generic;
using Battle.Units.Stats;
using UnityEngine;

namespace UI.Battle.HUD
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private HUDSpawner hudSpawner;
        [SerializeField] private StatHUDDataContainer statHUDDataContainer;

        [SerializeField] private float statChangeBufferTime;

        private readonly Dictionary<Stat, float> lastChangeTime = new();
        private readonly Dictionary<Stat, int> statValueChangeBuffer = new();
        private readonly Dictionary<Stat, StatHUDData> hudDataByStat = new();

        public void Start()
        {
            foreach ((Stat stat, StatHUDData statHUDData) in statHUDDataContainer.StatHUDDataList)
            {
                hudDataByStat.Add(stat, statHUDData);
                stat.OnValueChanged += value => CollectStatChangeData(stat, value);
            }
        }

        public void Update()
        {
            var statsToDeleteDataAbout = new List<Stat>();
            
            foreach ((Stat stat, float time) in lastChangeTime)
            {
                if (Time.time - time < statChangeBufferTime) continue;
                
                SpawnStatHUD(statValueChangeBuffer[stat], hudDataByStat[stat]);
                statsToDeleteDataAbout.Add(stat);
            }

            foreach (Stat stat in statsToDeleteDataAbout) DeleteStatBufferData(stat);
        }

        private void DeleteStatBufferData(Stat stat)
        {
            lastChangeTime.Remove(stat);
            statValueChangeBuffer.Remove(stat);
        }

        private void CollectStatChangeData(Stat stat, int value)
        {
            if (lastChangeTime.TryAdd(stat, Time.time))
            {
                statValueChangeBuffer.Add(stat, value);
            }
            else
            {
                lastChangeTime[stat] = Time.time;
                statValueChangeBuffer[stat] += value;
            }
        }
        
        private void SpawnStatHUD(int statValueChange, StatHUDData hudData)
        {
            hudSpawner.SpawnHUD(statValueChange.ToString(),
                hudData.ColorByStatValueChange(statValueChange),
                hudData.HUDMoveDirectionByStatValueChange(statValueChange));
        }
    }
}