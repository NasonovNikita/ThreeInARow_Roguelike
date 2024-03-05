using System;
using System.Collections.Generic;
using Battle.Units;
using UnityEngine;

namespace UI.Battle
{
    public class StateAnimationController : MonoBehaviour
    {
        [SerializeField]
        private RuntimeAnimatorController controller;

        [SerializeField] private Animator burningAnimator;
        [SerializeField] private Animator freezingAnimator;
        [SerializeField] private Animator poisoningAnimator;

        private readonly Dictionary<UnitStates, string> statesCodes = new ()
        {
            { UnitStates.Burning, "burning" },
            { UnitStates.Poisoning, "poisoning"},
            { UnitStates.Freezing, "frozen"}
        };

        public void Awake()
        {
            burningAnimator.runtimeAnimatorController = controller;
            freezingAnimator.runtimeAnimatorController = controller;
            poisoningAnimator.runtimeAnimatorController = controller;
        }

        public void AddState(UnitStates state)
        {
            SetStateFlag(state, true);
        }

        public void DeleteState(UnitStates state)
        {
            SetStateFlag(state, false);
        }

        private void SetStateFlag(UnitStates state, bool val)
        {
            GetAnimator(state).SetBool(statesCodes[state], val);
        }

        private Animator GetAnimator(UnitStates state)
        {
            return state switch
            {
                UnitStates.Burning => burningAnimator,
                UnitStates.Poisoning => poisoningAnimator,
                UnitStates.Freezing => freezingAnimator,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}