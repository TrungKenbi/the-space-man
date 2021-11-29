using System;
using Hero;
using Manager;
using UI;
using UnityEngine;
using Weapon.Data;

namespace Weapon.Logic
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        public BaseWeaponData weaponData;
        [HideInInspector]
        public Transform firePoint;

        public Action OnFired;
        public Action OnNotEnoughBullet;

        private HeroShooting _shooting;
        protected float interval;

        private void OnValidate()
        {
            if (firePoint == null)
                firePoint = transform.Find("FirePoint");
        }

        private void Awake()
        {
            _shooting = GetComponentInParent<HeroShooting>();
            if (OnFired == null)
            {
                OnFired += _shooting.PlayShootAudio;
                OnFired += _shooting.CameraShakeOnFire;
                OnFired += _shooting.OnGunFired;
            }

            if (OnNotEnoughBullet == null)
                OnNotEnoughBullet += RechargeBullet;
        }

        void Update()
        {
            if (interval > 0f)
            {
                interval -= Time.deltaTime;
                return;
            }

            if (InputManager.IsPressFire)
            {
                if (Firing())
                {
                    interval = weaponData.fireRate;
                    OnFired?.Invoke();
                }
            }
        }

        protected bool Firing()
        {
            if (_shooting.currentBulletsCount >= weaponData.numBulletsToFire)
            {
                _shooting.currentBulletsCount -= weaponData.numBulletsToFire;
                UIManager.Instance.SetBulletCount(_shooting.currentBulletsCount, _shooting.totalBulletsCount);
                weaponData.Fire(firePoint);

                if (_shooting.currentBulletsCount == 0)
                    OnNotEnoughBullet.Invoke();

                return true;
            }

            return false;
        }

        public void RechargeBullet()
        {
            if (_shooting.totalBulletsCount > 0)
            {
                StartCoroutine(_shooting.RechargeBulletAnimation());
            }
        }
    }
}
