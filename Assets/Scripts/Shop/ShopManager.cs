using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void Awake()
    {
        AudioManager.instance.StopAll();
        
        GameManager.instance.SaveData();
        
        AudioManager.instance.Play(AudioEnum.Shop);
    }
}