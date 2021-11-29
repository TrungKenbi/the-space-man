using System;
using Manager;
using UnityEngine;
using UnityEngine.UI;
using Weapon;
using Weapon.Data;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        public GUISkin guiSkin;
        
        [SerializeField]
        private Text bulletCount;
        
        [SerializeField]
        private Image weaponIcon;
        
        [SerializeField]
        private Text clock;
        
        [SerializeField]
        private Image[] healthCells;
        
        [SerializeField]
        private GameObject gameOver;
        
        [SerializeField]
        private GameObject pickupItem;
        
        [SerializeField]
        private GameObject pauseMenu;

        private bool _hasItemPickable;
        
        private int _numSeconds;

        private float _nextSecondTime = 0f;

        private void Update()
        {
            if (Time.time >= _nextSecondTime)
            {
                _nextSecondTime += 1f;
                _numSeconds++;
                clock.text = $"{(_numSeconds / 60):00}:{(_numSeconds % 60):00}";
            }
        }

        public void ChangeWeaponIcon(BaseWeaponData weapon)
        {
            weaponIcon.sprite = weapon.icon;
            weaponIcon.SetNativeSize();
        }
        
        public void SetBulletCount(int current, int total)
        {
            bulletCount.text = $"Bullets: {current} ({total})";
        }

        public void SetHealth(int currentHealth)
        {
            if (currentHealth < 0 || currentHealth > healthCells.Length)
                return;
            
            for(int i = 0; i < healthCells.Length; i++)
            {
                if (i + 1> currentHealth)
                {
                    healthCells[i].enabled = false;
                    continue;
                }

                healthCells[i].enabled = true;
            }
        }

        public void SetPickupItem(bool isShow)
        {
            // pickupItem.SetActive(isShow);
            _hasItemPickable = isShow;
        }

        public void GameOver()
        {
            gameOver.SetActive(true);
        }
        
        public void Restart()
        {
            gameOver.SetActive(false);
            ChangeWeaponIcon(GameManager.Instance.defaultWeapon);
            _numSeconds = 0;
            _nextSecondTime = 0f;
            SetHealth(5);
        }

        public void Pause(bool isPaused)
        {
            // pauseMenu.SetActive(isPaused);
        }
        
        void OnGUI()
        {
            GUI.skin = guiSkin;
            
            if (_hasItemPickable)
                OnPickupableItem();
            
            if (GameManager.Instance.isPaused)
                OnPauseUI();
        }

        private void OnPickupableItem()
        {
            Rect pauseMenuReact = new Rect (0, 0, 400, 380);
            pauseMenuReact.x = (Screen.width - pauseMenuReact.width)/2;
            pauseMenuReact.y = (Screen.height - pauseMenuReact.height) / 2;

            GUI.Box(pauseMenuReact, "");
            
            GUIStyle labelStyle =  new GUIStyle(GUI.skin.label)
            {
                fontSize = 28
            };
            
            GUI.Label (new Rect (pauseMenuReact.x + 50,pauseMenuReact.y + 50, 300, 80), "Press E to pickup item.", labelStyle);
            
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 38
            };

            bool btnPickupIsPress = GUI.Button (new Rect (pauseMenuReact.x + 50,pauseMenuReact.y + 150,300,80), "Pickup", buttonStyle);
            if (btnPickupIsPress)
                WeaponManager.Instance.ChangeWeapon();
        }

        private void OnPauseUI()
        {
            Rect pauseMenuReact = new Rect (0, 0, 400, 380);
            pauseMenuReact.x = (Screen.width - pauseMenuReact.width)/2;
            pauseMenuReact.y = (Screen.height - pauseMenuReact.height) / 2;

            GUI.Box(pauseMenuReact, "");
            
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 38
            };

            bool btnContinueIsPress = GUI.Button (new Rect (pauseMenuReact.x + 50,pauseMenuReact.y + 50,300,80), "Continue", buttonStyle);
            bool btnExitIsPress = GUI.Button (new Rect (pauseMenuReact.x + 50,pauseMenuReact.y + 150,300,80), "Exit", buttonStyle);
            
            if (btnContinueIsPress)
                GameManager.Instance.Pause();
            if (btnExitIsPress)
                GameManager.Instance.Exit();
        }
    }
}
