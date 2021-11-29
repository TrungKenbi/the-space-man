using System;
using Hero;
using UnityEngine;
using Weapon.Data;
using Weapon.Data.Melee;

namespace Weapon.Logic.Melee
{
    public abstract class BaseMeleeWeapon : MonoBehaviour
    {
        public BaseMeleeWeaponData weaponData;
        [HideInInspector] public Transform firePoint;

        public Action OnFired;

        private HeroShooting _shooting;
        protected float interval;

        private void Awake()
        {
            _shooting = GetComponentInParent<HeroShooting>();

            if (_shooting == null)
            {
                Debug.LogError("Can't find HeroShooting components in character !");
                return;
            }
            
            if (firePoint == null)
                firePoint = _shooting.gameObject.transform.Find("MeleeFirePoint");

            if (OnFired == null)
            {
                OnFired += _shooting.PlayShootAudio;
                OnFired += _shooting.CameraShakeOnFire;
                OnFired += _shooting.OnMeleeFired;
            }
        }

        void Update()
        {
            if (interval > 0f)
            {
                interval -= Time.deltaTime;
                return;
            }

            if (Input.GetMouseButtonDown(1))
            {
                weaponData.Fire(firePoint);
                interval = weaponData.fireRate;
                OnFired?.Invoke();
            }
        }
    }
}
