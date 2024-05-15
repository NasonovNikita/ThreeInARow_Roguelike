using Audio;
using Core.Saves;
using Other;
using UnityEngine;

namespace Treasure
{
    public class TreasureManager : MonoBehaviour
    {
        public static LootItem treasure;

        public void Awake()
        {
            AudioManager.Instance.StopAll();
            GameSave.Save();
            
            var treasureBox = FindFirstObjectByType<TreasureBox>();
            treasureBox.treasure = treasure;
            
            AudioManager.Instance.Play(AudioEnum.Treasure);
        }
    }
}