using System.Collections.Generic;
using Knot.Localization;
using UnityEngine;

namespace Map.PlotRooms
{
    public class PlotRoom : MonoBehaviour
    {
        [SerializeField] private KnotTextKeyReference textKey;

        protected int currentChoice;

        private List<PlotRoom> choices;
    }
}