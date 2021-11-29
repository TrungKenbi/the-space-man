using System;
using System.Collections;
using Hero;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;
using Weapon.Data;

namespace Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public GameObject defaultPlayerPrefab;
        public Transform respawnPoint;
        public FollowCamera followCamera;

        public BaseWeaponData defaultWeapon;

        [NonSerialized] public GameObject player;
        [NonSerialized] public HeroEntity heroEntity;

        [NonSerialized] public bool isPaused;

        private void OnValidate()
        {
            if (respawnPoint == null)
                respawnPoint = GameObject.FindWithTag("Respawn").transform;
        }

        protected override void Awake()
        {
            base.Awake();
            
            Application.quitting += OnQuitting;
        }

        private void Start()
        {
            CreateCharacter();
            Cursor.visible = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                LoadNextScene();
            }
        }

        public void LoadNextScene()
        {
            StartCoroutine(StartLoad());
        }

        private IEnumerator StartLoad()
        {
            var sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
            while (!operation.isDone)
            {
                yield return null;
            }
            CreateCharacter();
        }

        private void CreateCharacter()
        {
            if (respawnPoint == null)
                respawnPoint = GameObject.FindWithTag("Respawn").transform;
            
            player = Instantiate(defaultPlayerPrefab, respawnPoint.position, Quaternion.identity);
            heroEntity = player.GetComponent<HeroEntity>();
            followCamera.playerTransform = player.transform;
        }

        public void GameOver()
        {
            UIManager.Instance.GameOver();
            Time.timeScale = 0f;
            Cursor.visible = true;
            
            // Save game ...
        }

        public void Restart()
        {
            UIManager.Instance.Restart();
            Start();
            Time.timeScale = 1f;
            
            // Reset súng đạn, chuyển về màn cũ các kiểu con đà điểu
        }

        public void Pause()
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1f;
                isPaused = false;
            }
            UIManager.Instance.Pause(isPaused);
        }
        
        public void OnQuitting()
        {
            // Save all data
            Debug.Log("Done save all data !");
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
