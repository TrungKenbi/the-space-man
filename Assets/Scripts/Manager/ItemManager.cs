using DG.Tweening;
using Item;
using UnityEngine;
using Weapon.Data;

namespace Manager
{
    public class ItemManager : Singleton<ItemManager>
    {
        public GameObject defaultItemPrefab;

        private void DropItem(BaseItemData item, Vector3 location)
        {
            var itemDropped = Instantiate(defaultItemPrefab, location, Quaternion.identity);
            var control = itemDropped.GetComponent<ItemPickup>();
            control.data = item;
            control.Render();
            
            Vector3 spawnPosition = new Vector3(location.x + Random.Range(-1f, 1f), location.y + Random.Range(-1f, 1f), 0);
            itemDropped.transform.DOMove(spawnPosition, 0.3f).SetEase(Ease.OutFlash);
        }
        
        public void DropItem(BaseWeaponData weapon)
        {
            // DropItem(item);
        }

        public void EntityDeadDropItems(BaseItemData[] items, Vector3 location)
        {
            if (items.Length == 0)
                return;
            
            bool isAlreadySpawnWeapon = false;
            for (int i = 0; i < items.Length; i++)
            {
                if (isAlreadySpawnWeapon)
                    break;
                
                if (Random.value <= items[i].dropRate)
                {
                    DropItem(items[i], location);
                    if (items[i] is WeaponItemData)
                        isAlreadySpawnWeapon = true;
                }
            }   
        }
    }
}
