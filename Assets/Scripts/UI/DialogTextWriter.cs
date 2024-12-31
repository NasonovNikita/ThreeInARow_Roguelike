using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialogTextWriter : MonoBehaviour
    {
        [SerializeField] private Text text;

        [SerializeField] private float testingTimeDelay;

        private float TimeBetweenSymbols => Globals.Instance == null
            ? testingTimeDelay
            : Globals.Instance.timeBetweenSymbolsInPlotDialog;

        public void Start()
        {
            text.text = "";
        }

        public void Write(string content)
        {
            StartCoroutine(CoroutineWrite(content));
        }

        private IEnumerator CoroutineWrite(string content)
        {
            foreach (var c in content)
            {
                text.text = text.text + c;
                yield return new WaitForSeconds(TimeBetweenSymbols);
            }
        }
    }
}