using System;
using Manager;
using UI;
using UnityEngine;
using Weapon;

namespace Item
{
    public class ItemPickup : MonoBehaviour
    {
        public BaseItemData data;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private void OnValidate()
        {
            Render();
        }

        private void Awake()
        {
            Render();
        }

        public void Render()
        {
            spriteRenderer.sprite = data.icon;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (data.type)
            {
                case ItemType.Weapon:
                    var weapon = (WeaponItemData) data;
                    WeaponManager.Instance.WeaponChangeObject = gameObject;
                    WeaponManager.Instance.WeaponNeedChange = weapon.data;
                    UIManager.Instance.SetPickupItem(true);
                    break;

                default:
                    data.Activate(other);
                    Destroy(gameObject);
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            switch (data.type)
            {
                case ItemType.Weapon:
                    WeaponManager.Instance.WeaponChangeObject = null;
                    WeaponManager.Instance.WeaponNeedChange = null;
                    UIManager.Instance.SetPickupItem(false);
                    break;
            }
        }
    }
}
