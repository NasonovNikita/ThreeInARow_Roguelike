using Battle.Units;
using Knot.Localization;
using Other;
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

        public void Create(int moneyReward, Transform uiCanvas)
        {
            var window = Instantiate(this, uiCanvas);
            window.moneyRewardButtonText.text =
                moneyRewardTextKeyReference.Value.IndexErrorProtectedFormat(moneyReward);
            window.money = moneyReward;
        }

        public void GetMoney()
        {
            Player.data.money += money;
            Destroy(moneyRewardButton.gameObject);
        }
    }
}