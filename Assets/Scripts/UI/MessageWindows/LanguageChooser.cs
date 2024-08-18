using System.Linq;
using Knot.Localization;
using UnityEngine;

namespace UI.MessageWindows
{
    public class LanguageChooser : MonoBehaviour
    {
        public void SetEnglishLanguage()
        {
            SetLanguage(SystemLanguage.English);
            Close();
        }

        public void SetRussianLanguage()
        {
            SetLanguage(SystemLanguage.Russian);
            Close();
        }

        private void SetLanguage(SystemLanguage language)
        {
            var knotLanguageData = KnotLocalization.Manager.Languages
                .FirstOrDefault(d => d.SystemLanguage == language);
            if (knotLanguageData != null)
                KnotLocalization.Manager.LoadLanguage(knotLanguageData);
        }

        private void Close()
        {
            Destroy(gameObject);
        }
    }
}