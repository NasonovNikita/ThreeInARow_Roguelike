using System.Collections.Generic;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.UI.HUD
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private HUDSpawner hudSpawner;
        [SerializeField] private StatHUDDataContainer statHUDDataContainer;

        [SerializeField] private float statChangeBufferTime;
        private readonly Dictionary<Stat, StatHUDData> _hudDataByStat = new();

        private readonly Dictionary<Stat, float> _lastChangeTime = new();
        private readonly Dictionary<Stat, int> _statValueChangeBuffer = new();

        public void Start()
        {
            foreach (var (stat, statHUDData) in statHUDDataContainer
                         .StatHUDDataList)
            {
                _hudDataByStat.Add(stat, statHUDData);
                stat.OnValueChanged +=
                    value => CollectStatChangeData(stat, value);
            }
        }

        public void Update()
        {
            var statsToDeleteDataAbout = new List<Stat>();

            foreach (var (stat, time) in _lastChangeTime)
            {
                if (Time.time - time < statChangeBufferTime) continue;

                SpawnStatHUD(_statValueChangeBuffer[stat],
                    _hudDataByStat[stat]);
                statsToDeleteDataAbout.Add(stat);
            }

            foreach (var stat in statsToDeleteDataAbout)
                DeleteStatBufferData(stat);
        }

        private void DeleteStatBufferData(Stat stat)
        {
            _lastChangeTime.Remove(stat);
            _statValueChangeBuffer.Remove(stat);
        }

        private void CollectStatChangeData(Stat stat, int value)
        {
            if (_lastChangeTime.TryAdd(stat, Time.time))
            {
                _statValueChangeBuffer.Add(stat, value);
            }
            else
            {
                _lastChangeTime[stat] = Time.time;
                _statValueChangeBuffer[stat] += value;
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