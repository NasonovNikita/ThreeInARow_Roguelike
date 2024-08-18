using System.Linq;
using Battle.Grid;
using Battle.Units;
using Knot.Localization;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MessageWindows
{
    public class BattleWinWindow : MonoBehaviour
    {
        [SerializeField] private Button cellRewardButton;
        [SerializeField] private InfoObject cellInfoObject;

        [SerializeField] private Button moneyRewardButton;
        [SerializeField] private Text moneyRewardButtonText;

        [SerializeField] private KnotTextKeyReference moneyRewardTextKeyReference;
        private Cell _cell;
        private int _money;

        public void Create(int moneyReward, Transform uiCanvas)
        {
            var window = Instantiate(this, uiCanvas);

            window.moneyRewardButtonText.text =
                moneyRewardTextKeyReference.Value.IndexErrorProtectedFormat(moneyReward);
            window._money = moneyReward;

            var possibleToAddCells = Resources.LoadAll<Cell>("Prefabs/Battle/Cells")
                .Where(loadedCell =>
                    !Player.Data.cells.Exists(playerCell =>
                        playerCell.IsSameType(loadedCell)) &&
                    loadedCell.possibleReward).ToList();

            if (possibleToAddCells.Count == 0)
            {
                Destroy(window.cellRewardButton.gameObject);
            }
            else
            {
                window._cell = Tools.Random.RandomChoose(possibleToAddCells);
                window.cellRewardButton.image.sprite =
                    window._cell.GetComponent<Image>().sprite;
                window.cellInfoObject.text = window._cell.Description; // TODO
            }
        }

        public void GetMoney()
        {
            Player.Data.money += _money;
            Destroy(moneyRewardButton.gameObject);
        }

        public void GetCell()
        {
            Player.Data.cells.Add(_cell);
            Destroy(cellRewardButton.gameObject);
        }
    }
}