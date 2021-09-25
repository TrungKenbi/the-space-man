﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShooting : BaseMonoBehaviour
{
    public GameObject weaponAndHands;
    public Transform firePoint;
    public BaseWeaponData weaponData;

    public float cameraShakeDuration = 0.05f;
    public float cameraShakeStrength = 0.15f;

    [SerializeField]
    private AudioSource shootingAudio;

    private float interval = 0f;

    private void OnValidate()
    {
        if (shootingAudio == null)
            shootingAudio = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        InitWeapon();
    }

    public void InitWeapon()
    {
        // Remove all child gameObject in weaponAndHands
        foreach (Transform child in weaponAndHands.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        // Add weapon to character
        Instantiate(weaponData.weaponPrefab, weaponAndHands.transform);

        // Detect firepoint in weapon prefab
        GameObject firePointObject = GameObject.FindGameObjectWithTag("FirePoint");
        if (!firePointObject)
        {
            Debug.LogError("Can't find fire point in character !");
            return;
        }
        firePoint = GameObject.FindGameObjectWithTag("FirePoint")?.transform;
    }

    public override void DoUpdate()
    {
        Turning();

        if (interval > 0f)
        {
            interval -= Time.deltaTime;
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            interval = weaponData.fireRate;
        }
    }

    private void Turning()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direct = new Vector2(mousePos.x - weaponAndHands.transform.position.x, mousePos.y - weaponAndHands.transform.position.y);
        

        if (mousePos.x > transform.position.x + 0.1f)
        {
            weaponAndHands.transform.right = direct;
            return;
        }

        if (mousePos.x < transform.position.x - 0.1f)
        {
            weaponAndHands.transform.right = -direct;
            return;
        }
    }

    private void Shoot()
    {
        weaponData.Fire(firePoint);
        PlayShootAudio();
        CameraShakeOnFire();
    }

    private void PlayShootAudio()
    {
        // _shootingAudio.Stop();
        shootingAudio.Play();
    }

    private void CameraShakeOnFire()
    {
        Camera.main.DOShakePosition(cameraShakeDuration, cameraShakeStrength, 10, 90, false);
    }
}
