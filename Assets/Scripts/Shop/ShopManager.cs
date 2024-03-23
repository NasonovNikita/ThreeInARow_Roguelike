using System;
using System.Collections.Generic;
using Audio;
using Core.Saves;
using Other;
using UnityEngine;

namespace Shop
{
    public class ShopManager : MonoBehaviour
    {
        public static List<Good> goods = new();
        public static float salePrice = 0.8f;
        public static bool entered;
        public void Awake()
        {
            AudioManager.instance.StopAll();
        
            GameSave.Save();

            var goodBoxes = FindObjectsByType<GoodBox>(FindObjectsSortMode.None);
            for (int i = 0; i < goods.Count && i < 4; i++)
            {
                goodBoxes[i].good = goods[i];
            }
            Tools.InstantiateAll(goods);
            if (goods.Count == 0) throw new Exception("No goods found. Are they loaded correctly?");
            if (entered) goods[0].price = (int) (goods[0].price * salePrice);
            entered = false;
            AudioManager.instance.Play(AudioEnum.Shop);
        }
    }
}