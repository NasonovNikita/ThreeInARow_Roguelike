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
        
        private readonly Dictionary<UnitStates, Animator> currentStates = new();

        private readonly Dictionary<UnitStates, string> statesCodes = new ()
        {
            { UnitStates.Burning, "burning" }
        };

        public void AddState(UnitStates state)
        {
            if (currentStates.ContainsKey(state)) return;
            
            Animator animator = Instantiate(animatorPrefab, transform);
            animator.runtimeAnimatorController = controller;
            animator.SetBool(statesCodes[state], true);
            currentStates.Add(state, animator);
        }

        public void DeleteState(UnitStates state)
        {
            currentStates[state].SetBool(statesCodes[state], false);
        }
    }
}