using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private GameObject _playerPrefab;

    // Unit is the parent class of all Enemy and BossEnemy
    [SerializeField] private List<Unit> _enemies;

    private void Start()
    {
        SpawnEnemies(_bossPrefab, "Bobert", 200, 2f);
    }
    
    private void SpawnEnemies(GameObject prefab, string name, int maxhealth, float speed)
    {
        float randomX;
        float randomY;
        Vector3 position = new Vector3();
        position.z = 0;

        randomX = Random.Range(-50, 50);
        randomY = Random.Range(-5, 5);
        position.x = gameObject.transform.position.x + randomX;
        position.y = gameObject.transform.position.y + randomY;

        GameObject unitGO = Instantiate(prefab, position, Quaternion.identity);
        unitGO.transform.parent = transform;

        Unit unit = unitGO.GetComponent<Unit>();
        _enemies.Add(unit);

        unit.Initialize(name, maxhealth, speed);

        EnemyMovement unitMove = unitGO.GetComponent<EnemyMovement>();
        unitMove._playerTarget = _playerPrefab;
    }
}