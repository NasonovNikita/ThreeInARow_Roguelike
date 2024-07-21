#if UNITY_EDITOR

using Node = Map.Nodes.Node;

namespace DevTools.DebugLogging.LoggingGroups.Map
{
    public class NodesLoggingGroup : DebugLoggingGroup
    {
        public override void Attach()
        {
            Node.OnArrive += WriteNodeArrival;
            Node.OnClicked += WriteNodeClicked;
        }

        public override void UnAttach()
        {
            Node.OnArrive -= WriteNodeArrival;
            Node.OnClicked -= WriteNodeClicked;
        }

        private void WriteNodeArrival(Node node)
        {
            CheckAndWrite($"Arrived to node {node}");
        }

        private void WriteNodeClicked(Node node)
        {
            CheckAndWrite($"Clicked at node {node}");
        }
    }
}

#endif