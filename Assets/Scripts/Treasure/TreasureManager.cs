using Audio;
using Core;
using Other;
using UnityEngine;

namespace Treasure
{
    public class TreasureManager : MonoBehaviour
    {
        public static GetAble treasure;

        public void Awake()
        {
            AudioManager.instance.StopAll();
            GameManager.instance.SaveData();
            
            var treasureBox = FindFirstObjectByType<TreasureBox>();
            treasureBox.treasure = treasure;
            
            AudioManager.instance.Play(AudioEnum.Treasure);
        }
    }
}