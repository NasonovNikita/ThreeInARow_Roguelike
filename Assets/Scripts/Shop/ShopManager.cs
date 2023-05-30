using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static List<Good> goods;
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