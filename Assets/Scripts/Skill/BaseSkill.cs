using UnityEngine;

namespace Skill
{
    public abstract class BaseSkill : ScriptableObject
    {
        public abstract void Active(Vector3 firePoint);
    }
}
