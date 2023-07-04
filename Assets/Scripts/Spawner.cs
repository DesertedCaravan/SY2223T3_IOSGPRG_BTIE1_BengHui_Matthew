using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _bossPrefab;

    // Unit is the parent class of all Enemy and BossEnemy
    [SerializeField] private List<Unit> _enemies;

    private void Start()
    {
        SpawnEnemies(5, _enemyPrefab, "Bob", 100, 5f);
        SpawnEnemies(1, _bossPrefab, "Bobert", 200, 2.5f);
    }
    
    private void SpawnEnemies(int count, GameObject prefab, string name, int maxhealth, float speed)
    {
        float randomX;
        float randomY;
        Vector3 position = new Vector3();
        position.z = 0;

        for (int i = 0; i < count; i++)
        {
            randomX = Random.Range(-5, 5);
            randomY = Random.Range(-5, 5);
            position.x = randomX;
            position.y = randomY;

            GameObject unitGO = Instantiate(prefab, position, Quaternion.identity);
            unitGO.transform.parent = transform;

            Unit unit = unitGO.GetComponent<Unit>();

            _enemies.Add(unit);

            unit.Initialize(name, maxhealth, speed);
        }
    }
}