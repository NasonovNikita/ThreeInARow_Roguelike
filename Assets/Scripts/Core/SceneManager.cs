using Audio;
using Core.Saves;
using UnityEngine;

namespace Core
{
    // Temporary is not in use
    public class SceneManager : MonoBehaviour
    {
        public void Start()
        {
            AudioManager.Instance.StopAll();

            AudioManager.Instance.Play(AudioEnum.MainMenu);

            SettingsSave.Save();
        }
    }
}