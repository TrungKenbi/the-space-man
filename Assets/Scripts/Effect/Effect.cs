using ECS;
using UnityEngine;

namespace Effect
{
    public class Effect
    {
        public EffectLogic logic;
        private float _currentDuration;

        public Effect(EffectLogic logic, float amount)
        {
            this.logic = logic;
            _currentDuration = logic.duration;
        }

        public bool Tick(BaseEntity entity)
        {
            if (_currentDuration <= 0f)
                return false;

            _currentDuration -= Time.deltaTime;
            
            logic.Tick(entity);
            return true;
        }
    }
}
