using Audio;
using Core.Saves;
using Other;
using UnityEngine;

namespace Treasure
{
    public class SceneManager : MonoBehaviour
    {
        public static LootItem Treasure;

        public void Awake()
        {
            AudioManager.Instance.StopAll();
            GameSave.Save();

            var treasureBox = FindFirstObjectByType<TreasureBox>();
            treasureBox.treasure = Treasure;

            AudioManager.Instance.Play(AudioEnum.Treasure);
        }
    }
}