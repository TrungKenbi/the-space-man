using UnityEngine;

namespace Manager
{
    public class Spawner : MonoBehaviour
    {
        public GameObject[] monstersPrefab;
        public GameObject[] monsterSpawns;

        private void OnValidate()
        {
            // if (monsterSpawns == null)
                monsterSpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
        }

        private void Start()
        {
            foreach (GameObject monster in monsterSpawns)
            {
                Instantiate(monstersPrefab[Random.Range(0, monstersPrefab.Length)], monster.transform.position, monster.transform.rotation);
            }
        }
    }
}
