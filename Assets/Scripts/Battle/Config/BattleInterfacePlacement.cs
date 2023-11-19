using System.Collections.Generic;
using UnityEngine;

namespace Battle.Config
{
    public class BattleInterfacePlacement : MonoBehaviour
    {
        public List<BattleConfig> configs = new ();

        [SerializeField]
        private BattleConfigs chosenConfig;

        public void Place()
        {
            BattleConfig cfg = configs.Find(val => val.mark == chosenConfig);
            cfg.Apply();
        }
    }
}