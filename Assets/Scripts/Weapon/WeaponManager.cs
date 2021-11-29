using System;
using System.Collections.Generic;
using Manager;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon.Data;

namespace Weapon
{
    public class WeaponManager : Singleton<WeaponManager>
    {
        [NonSerialized] public BaseWeaponData WeaponNeedChange;
        [NonSerialized] public GameObject WeaponChangeObject;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                ChangeWeapon();
        }

        public void ChangeWeapon()
        {
            if (WeaponNeedChange != null)
            {
                GameManager.Instance.heroEntity.ChangeWeapon(WeaponNeedChange);
                Destroy(WeaponChangeObject);
            }
        }
    }
}
