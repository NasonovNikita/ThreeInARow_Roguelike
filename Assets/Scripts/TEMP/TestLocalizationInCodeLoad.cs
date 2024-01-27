using System.Linq;
using Knot.Localization;
using Knot.Localization.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TEMP
{
    public class TestLocalizationInCodeLoad : MonoBehaviour
    {
        private Text text;

        public void Start()
        {
            text = GetComponent<Text>();
            KnotLanguageData targetLanguage = KnotLocalization.Manager.Languages.FirstOrDefault(d => d.SystemLanguage ==
                SystemLanguage.Russian);
            if (targetLanguage != null)
                KnotLocalization.Manager.LoadLanguage(targetLanguage);
            text.text = KnotLocalization.GetText("Continue");
        }
    }
}