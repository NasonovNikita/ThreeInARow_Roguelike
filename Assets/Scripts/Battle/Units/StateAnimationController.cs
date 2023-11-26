using System.Collections.Generic;
using UnityEngine;

namespace Battle.Units
{
    public class StateAnimationController : MonoBehaviour
    {
        [SerializeField]
        private RuntimeAnimatorController controller;

        [SerializeField]
        private Animator animatorPrefab;

        private Dictionary<UnitStates, Animator> currentStates = new();

        private readonly Dictionary<UnitStates, string> statesCodes = new ()
        {
            { UnitStates.Burning, "burning" },
            { UnitStates.Poisoning, "poisoning"},
            { UnitStates.Frozen, "frozen"}
        };

        public void Awake()
        {
            currentStates = new Dictionary<UnitStates, Animator>
            {
                { UnitStates.Burning, Instantiate(animatorPrefab, transform)},
                { UnitStates.Poisoning, Instantiate(animatorPrefab, transform)},
                { UnitStates.Frozen, Instantiate(animatorPrefab, transform)}
            };
            
            foreach (Animator animator in currentStates.Values)
            {
                animator.runtimeAnimatorController = controller;
            }
        }

        public void AddState(UnitStates state)
        {
            currentStates[state].SetBool(statesCodes[state], true);
        }

        public void DeleteState(UnitStates state)
        {
            currentStates[state].SetBool(statesCodes[state], false);
        }
    }
}