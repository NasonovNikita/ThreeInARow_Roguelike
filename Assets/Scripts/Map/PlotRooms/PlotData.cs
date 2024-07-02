using System;
using System.Collections.Generic;
using Knot.Localization;

namespace Map.PlotRooms
{
    [Serializable]
    public struct PlotData
    {
        public KnotTextKeyReference text;

        public List<string> actions;
        public List<KnotTextKeyReference> actionsNamesKeys;

        public List<PlotData> next;
    }
}