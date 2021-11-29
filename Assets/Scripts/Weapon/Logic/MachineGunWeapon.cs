using UnityEngine;

namespace Weapon.Logic
{
    public class MachineGunWeapon : BaseWeapon
    {
        private bool _isFire;
    
        public void Update()
        {
            if (Input.GetButtonUp("Fire1"))
                _isFire = false;

            if (interval > 0f)
            {
                interval -= Time.deltaTime;
                return;
            }

            if (Input.GetButtonDown("Fire1"))
                _isFire = true;

            if (_isFire)
            {
                if (Firing())
                {
                    interval = weaponData.fireRate;
                    OnFired?.Invoke();
                }
            }
        }
    }
}
