﻿using System.Collections.Generic;
using UnityEngine;

namespace SGoap
{
    public class AgentStateMonitor : MonoBehaviour
    {
        [Header("Pre-load agent with states")]
        [EffectAndValue]
        public List<State> PreStates;

        [Header("Add effects to actions")]
        public List<ActionEffect> PrePerformEffects;
        public List<PerformEffect> PerformEffects;
        public List<ActionEffect> PostPerformEffects;

        private Dictionary<Action, float> _actionStartTime = new Dictionary<Action, float>();

        private void Awake()
        {
            foreach (var effect in PrePerformEffects)
                effect.Action.OnPrePerform += () => { Set(effect); };

            foreach (var effect in PostPerformEffects)
                effect.Action.OnPostPerform += () => { Set(effect); };

            foreach (var effect in PerformEffects)
            {
                effect.Action.OnPrePerform += () => { _actionStartTime[effect.Action] = Time.time; };

                effect.Action.OnPerform += () =>
                {
                    var timeElapsed = Time.time - _actionStartTime[effect.Action];

                    if (timeElapsed >= effect.Rate)
                    {
                        Set(effect);
                        _actionStartTime[effect.Action] = Time.time;
                    }
                };
            }
        }

        public void Set(ActionEffect effect)
        {
            var states = effect.Space == Space.Self ? effect.Action.States : World.Instance.States;
            GOAPUtils.SetState(effect, states);
        }
    }
}