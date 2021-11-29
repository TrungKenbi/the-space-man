using System;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace Hero
{
    public class HeroEffect : BaseComponent
    {
        private List<Effect.Effect> _currentEffects;
        private Queue<Effect.Effect> _removeEffects;

        private void Awake()
        {
            _currentEffects = new List<Effect.Effect>();
            _removeEffects = new Queue<Effect.Effect>();
        }

        public override void DoUpdate()
        {
            foreach (var effect in _currentEffects)
            {
                if (!effect.Tick(entity))
                    _removeEffects.Enqueue(effect);
            }

            while (_removeEffects.Count > 0)
            {
                Effect.Effect effect = _removeEffects.Dequeue();
                RemoveEffect(effect);
            }
        }

        public void AddEffect(Effect.Effect effect)
        {
            _currentEffects.Add(effect);
            effect.logic.Activation(entity);
        }

        public void RemoveEffect(Effect.Effect effect)
        {
            effect.logic.Deactivation(entity);
            _currentEffects.Remove(effect);
        }
    }
}
