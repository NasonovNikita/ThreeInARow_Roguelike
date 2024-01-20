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
            AudioManager.instance.StopAll();
            SavesManager.SaveGame();
            
            var treasureBox = FindFirstObjectByType<TreasureBox>();
            treasureBox.treasure = treasure;
            
            AudioManager.instance.Play(AudioEnum.Treasure);
        }
    }
}