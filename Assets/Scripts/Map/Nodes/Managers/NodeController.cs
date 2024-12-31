using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Map.Nodes.Managers
{
    public class NodeController : MonoBehaviour
    {
        private List<Node> _nodes = new();

        [NonSerialized] public int CurrentNodeIndex;

        [NonSerialized] public bool NoNodeIsChosen;

        [NonSerialized] public bool Unlocked; // For debug

        public static NodeController Instance { get; private set; }

        private Node CurrentNode => _nodes[CurrentNodeIndex];

        private bool CameOutFromFinalNode => CurrentNodeIndex == _nodes.Count - 1;

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

        /// <summary>
        ///     Get new node tree for controller.
        /// </summary>
        public void RegenerateNodes()
        {
            foreach (var node in _nodes.Where(node => node != null))
                Destroy(node.gameObject); // Delete old nodes if were spawned

            _nodes = Generator.Instance.GetMap(Globals.Instance.seed);
        }

        public bool TryArrive(Node node)
        {
            var success = (NoNodeIsChosen && _nodes.IndexOf(node) == 0) ||
                          (!NoNodeIsChosen && CurrentNode.BelongsToNext(node));

            success = success || Unlocked;

            if (!success) return false;

            if (!NoNodeIsChosen) CurrentNode.UnChoose();
            SetCurrentNode(node);

            return true;
        }

        private void SetCurrentNode(Node node)
        {
            CurrentNodeIndex = _nodes.IndexOf(node);
            NoNodeIsChosen = false;
        }

        private void ScaleCurrentNode()
        {
            if (!NoNodeIsChosen) CurrentNode.Choose();
        }

        private void ShowNodes()
        {
            foreach (var node in _nodes)
                node.gameObject.SetActive(true);
        }

        private void HideNodes()
        {
            foreach (var node in _nodes)
                node?.gameObject.SetActive(false);
        }
    }
}