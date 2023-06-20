using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private GameObject enemyPrefab;
    // create parent for the spawned enemies
    [SerializeField] private Transform enemyParent;
    [SerializeField] private List<GameObject> enemies;

    private bool _active = false;

    bool _superMode = false;
    bool _superModeSetUp = false;
    float _spawnRate = 1.0f;

    public void GameStart()
    {
        StartCoroutine(CO_Spawner());
    }

    public void GameOn()
    {
        _active = true;
    }

    public void GamePause()
    {
        _active = false;
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

    private void Spawn(int count)
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
                // 33% chance to be aerial
                _randomXPosition = -1.5f;
            }
            else
            {
                _randomXPosition = -0.5f;
            }

            Vector3 randomPosition = new Vector3(_randomXPosition, 7.0f, -0.5f);
            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            enemy.transform.parent = enemyParent;
            enemies.Add(enemy);

            if (_superModeSetUp == true)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.ActivateSuperMode();
            }
        }
    }

    private IEnumerator CO_Spawner()
    {
        while (_active)
        {
            _spawnRate = Random.Range(1, 3);

            yield return new WaitForSeconds(_spawnRate);

            Spawn(1);
        }
    }
}
