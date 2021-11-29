using ECS;
using UnityEngine;

namespace Effect
{
    public abstract class EffectLogic : ScriptableObject
    {
        public new EffectName name;
        public float duration;
        
        public abstract void Activation(BaseEntity entity);
        public abstract void Tick(BaseEntity entity);
        public abstract void Deactivation(BaseEntity entity);
    }
}
