using UnityEngine;

namespace ECS
{
    public abstract class BaseComponent : MonoBehaviour
    {
        public BaseEntity entity;

        public virtual void DoUpdate()
        {
            
        }
    }
}
