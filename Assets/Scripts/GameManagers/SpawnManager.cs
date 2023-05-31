using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnedParent; // create parent for the spawned enemies
    [SerializeField] private List<GameObject> enemies;

    [SerializeField] private bool _gameOn = true;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(CO_EnemySpawn());

        // SpawnEnemies(1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GameOn()
    {
        _gameOn = true;
    }

    public void GamePause()
    {
        _gameOn = false;
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    private void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float _xPositionSelect = Random.Range(0, 3);
            float _randomXPosition = 0;

            if (_xPositionSelect == 0)
            {
                _randomXPosition = -1.5f; // 33% chance to be aerial
            }
            else
            {
                _randomXPosition = -0.5f;
            }

            Vector3 randomPosition = new Vector3(_randomXPosition, 7.0f, -0.5f);

            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            enemy.transform.parent = spawnedParent;

            enemies.Add(enemy);

            Enemy enemyScript = enemy.GetComponent<Enemy>(); // Due to this script being a Singleton
            enemyScript._attack = 10;
            enemyScript._defense = 10;
            enemyScript._health = Random.Range(5, 10);
        }
    }

    private IEnumerator CO_EnemySpawn()
    {
        while (_gameOn)
        {
            yield return new WaitForSeconds(1.0f);

            SpawnEnemies(1);
        }
    }
}
