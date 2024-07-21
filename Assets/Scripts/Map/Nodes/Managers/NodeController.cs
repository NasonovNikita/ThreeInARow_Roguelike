using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Map.Nodes.Managers
{
    public class NodeController : MonoBehaviour
    {
#if UNITY_EDITOR

        public bool unlocked;

#endif

        [NonSerialized] public int currentNodeIndex;

        private List<Node> nodes = new();

        [NonSerialized] public bool noNodeIsChosen;
        public static NodeController Instance { get; private set; }

        private Node CurrentNode => nodes[currentNodeIndex];

        private bool CameOutFromFinalNode => currentNodeIndex == nodes.Count - 1;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public event Action OnCameOutFromFinalNode;

        public void OnSceneEnter()
        {
            ShowNodes();
            ScaleCurrentNode();

            if (CameOutFromFinalNode) OnCameOutFromFinalNode?.Invoke();
        }

        public void OnSceneLeave()
        {
            HideNodes();
        }

        public void RegenerateNodes()
        {
            foreach (Node node in nodes.Where(node => node != null)) Destroy(node.gameObject);

            nodes = Generator.Instance.GetMap(Globals.Instance.seed);
        }

        public bool TryArrive(Node node)
        {
            var success = (noNodeIsChosen && nodes.IndexOf(node) == 0) ||
                          (!noNodeIsChosen && CurrentNode.BelongsToNext(node));

#if UNITY_EDITOR

            success = success || unlocked;

#endif

            if (!success) return false;

            if (!noNodeIsChosen) CurrentNode.UnChoose();
            SetCurrentNode(node);

            return true;
        }

        private void SetCurrentNode(Node node)
        {
            currentNodeIndex = nodes.IndexOf(node);
            noNodeIsChosen = false;
        }

        private void ScaleCurrentNode()
        {
            if (!noNodeIsChosen) CurrentNode.Choose();
        }

        private void ShowNodes()
        {
            foreach (Node node in nodes)
                node.gameObject.SetActive(true);
        }

        private void HideNodes()
        {
            foreach (Node node in nodes)
                node?.gameObject.SetActive(false);
        }
    }
}