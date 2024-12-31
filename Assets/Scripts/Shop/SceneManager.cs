using System;
using System.Collections.Generic;
using Audio;
using Core.Saves;
using Other;
using UnityEngine;

namespace Shop
{
    public class SceneManager : MonoBehaviour
    {
        public static List<Good> Goods = new();
        public static float SalePrice = 0.8f;
        public static bool Entered;

        public void Awake()
        {
            AudioManager.Instance.StopAll();

            GameSave.Save();

            var goodBoxes = FindObjectsByType<GoodBox>(FindObjectsSortMode.None);
            for (var i = 0; i < Goods.Count && i < 4; i++) goodBoxes[i].good = Goods[i];
            Tools.InstantiateAll(Goods);
            if (Goods.Count == 0)
                throw new Exception("No goods found. Are they loaded correctly?");
            if (Entered) Goods[0].price = (int)(Goods[0].price * SalePrice);
            Entered = false;
            AudioManager.Instance.Play(AudioEnum.Shop);
        }
    }
}