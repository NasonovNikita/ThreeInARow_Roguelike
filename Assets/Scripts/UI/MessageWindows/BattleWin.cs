using Battle.Units;
using Knot.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MessageWindows
{
    public class BattleWin : MonoBehaviour
    {
        [SerializeField] private Button moneyRewardButton;
        [SerializeField] private Text moneyRewardButtonText;
        [SerializeField] private KnotTextKeyReference moneyRewardTextKeyReference;
        private int money;

        public void Create(int moneyReward)
        {
            var window = Instantiate(this);
            window.moneyRewardButtonText.text =
                string.Format(moneyRewardTextKeyReference.Value, moneyReward);
            window.money = moneyReward;
        }

        public void GetMoney()
        {
            Player.data.money += money;
            Destroy(moneyRewardButton.gameObject);
        }
    }
}