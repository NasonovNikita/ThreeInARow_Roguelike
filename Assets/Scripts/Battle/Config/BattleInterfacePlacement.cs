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
            //TEMP          UPD: not temp if there won't be any other configs
            chosenConfig = Globals.instance.altBattleUI ? BattleConfigs.BigGridConfig : BattleConfigs.BaseConfig;
            //TEMP
            BattleConfig cfg = configs.Find(val => val.mark == chosenConfig);
            cfg.Apply();
        }
    }
}