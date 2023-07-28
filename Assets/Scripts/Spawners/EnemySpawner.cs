using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _playerPrefab;

    // Unit is the parent class of all Enemy and BossEnemy
    [SerializeField] private List<Unit> _enemies;

    private void Start()
    {
        SpawnEnemies(5, _enemyPrefab, "Bob", 100, 3.5f);
    }
    
    private void SpawnEnemies(int count, GameObject prefab, string name, int maxhealth, float speed)
    {
        float randomX;
        float randomY;
        Vector3 position = new Vector3();
        position.z = 0;

        for (int i = 0; i < count; i++)
        {
            randomX = Random.Range(-10, 10);
            randomY = Random.Range(-10, 10);
            position.x = gameObject.transform.position.x + randomX;
            position.y = gameObject.transform.position.y + randomY;

            GameObject unitGO = Instantiate(prefab, position, Quaternion.identity);
            unitGO.transform.parent = transform;

            Unit unit = unitGO.GetComponent<Unit>();
            _enemies.Add(unit);

            unit.Initialize(name, maxhealth, speed);

            EnemyMovement unitMove = unitGO.GetComponent<EnemyMovement>();
            unitMove._playerTarget = _playerPrefab;
            unitMove._enemyTarget = _enemyPrefab;
        }
    }
}