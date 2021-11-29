using Effect;
using Hero;
using Manager;
using UnityEngine;

namespace Item.Data
{
    [CreateAssetMenu(menuName = "Items/Gate")]
    public class GateItemData : BaseItemData
    {
        public override void Activate(Collider2D other)
        {
            GameManager.Instance.LoadNextScene();
        }

        public override void Deactivate(Collider2D other)
        {
            
        }
    }
}
