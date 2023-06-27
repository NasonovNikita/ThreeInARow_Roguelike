using System.Collections.Generic;
using Audio;
using UI;
using UnityEngine;

namespace Shop
{
    public class ShopManager : MonoBehaviour
    {
        public static List<Good> goods = new();
        public void Awake()
        {
            AudioManager.instance.StopAll();
        
            GameManager.instance.SaveData();

            var goodBoxes = FindObjectsByType<GoodBox>(FindObjectsSortMode.None);
            for (int i = 0; i < goods.Count && i < 4; i++)
            {
                goodBoxes[i].good = goods[i];
            }

            AudioManager.instance.Play(AudioEnum.Shop);
        }
    }
}