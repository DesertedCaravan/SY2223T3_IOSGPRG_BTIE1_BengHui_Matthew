using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnGunsPrefabs;
    [SerializeField] private List<GameObject> _spawnAmmoPrefabs;
    [SerializeField] private GameObject _spawnMedkitPrefabs;

    public virtual void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        int amount = Random.Range(3, 6);

        amount *= 28;

        SpawnPickups(amount, 1);
    }

    public void SpawnPickups(int count, int mode)
    {
        float randomX = 0;
        float randomY = 0;
        Vector3 position = new Vector3();
        position.z = 0;

        int rate;

        GameObject chosenPrefab;

        for (int i = 0; i < count; i++)
        {
            if (mode == 1)
            {
                randomX = Random.Range(-100, 100);
                randomY = Random.Range(-50, 50);
            }
            else if (mode == 2)
            {
                randomX = Random.Range(-5, 5);
                randomY = Random.Range(-5, 5);
            }
            
            position.x = gameObject.transform.position.x + randomX;
            position.y = gameObject.transform.position.y + randomY;

            rate = Random.Range(0, 10);

            chosenPrefab = _spawnGunsPrefabs[0];

            if (mode == 1)
            {
                if (rate >= 0 && rate <= 2)
                {
                    chosenPrefab = _spawnGunsPrefabs[Random.Range(0, _spawnGunsPrefabs.Count)];
                }
                else if (rate >= 3 && rate <= 9)
                {
                    chosenPrefab = _spawnAmmoPrefabs[Random.Range(0, _spawnAmmoPrefabs.Count)];
                }

                rate = Random.Range(0, 10);

                if (rate == 0)
                {
                    chosenPrefab = _spawnMedkitPrefabs;
                }
            }
            else if (mode == 2)
            {
                if (rate == 0)
                {
                    chosenPrefab = _spawnGunsPrefabs[Random.Range(0, _spawnGunsPrefabs.Count)];
                }
                else if (rate >= 1 && rate <= 3)
                {
                    chosenPrefab = _spawnMedkitPrefabs;
                }
                else
                {
                    chosenPrefab = _spawnAmmoPrefabs[Random.Range(0, _spawnAmmoPrefabs.Count)];
                }
            }

            GameObject pickupGO = Instantiate(chosenPrefab, position, Quaternion.identity);

            ChangeParent(pickupGO);
        }
    }

    public virtual void ChangeParent(GameObject pickup)
    {
        pickup.transform.parent = transform;
    }
}
