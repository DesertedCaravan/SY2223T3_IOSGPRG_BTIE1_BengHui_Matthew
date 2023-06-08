using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnedParent; // create parent for the spawned enemies
    [SerializeField] private List<GameObject> enemies;

    private bool _gameOn = false;
    private bool _coroutine = false;

    bool _superMode = false;
    bool _superModeSetUp = false;
    float _spawnRate = 1.0f;

    void Update()
    {
        if (_gameOn == true)
        {
            StartCoroutine(CO_EnemySpawn());

            _gameOn = false;
        }
    }

    public void GameOn()
    {
        _gameOn = true;
    }

    public void CorouteOn()
    {
        _coroutine = true;
    }

    public void CoroutePause()
    {
        _coroutine = false;
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public void ActivateSuperMode()
    {
        _superMode = true;

        _spawnRate = 0.5f;
    }

    public void DeactivateSuperMode()
    {
        _superModeSetUp = false;

        _spawnRate = 1.0f;
    }

    private void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (_superMode == true && _superModeSetUp == false)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    Enemy previousEnemy = enemies[j].GetComponent<Enemy>();
                    previousEnemy.ActivateSuperMode();
                }

                _superMode = false;
                _superModeSetUp = true;
            }

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

            if (_superModeSetUp == true)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.ActivateSuperMode();
            }
        }
    }

    public void RemoveAllEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies.RemoveAt(i);
        }
    }

    private IEnumerator CO_EnemySpawn()
    {
        while (_coroutine)
        {
            yield return new WaitForSeconds(_spawnRate);

            SpawnEnemies(1);
        }
    }
}
