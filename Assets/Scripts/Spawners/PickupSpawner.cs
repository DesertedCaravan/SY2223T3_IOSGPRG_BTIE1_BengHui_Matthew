using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _pistolPrefab;
    [SerializeField] private GameObject _automaticRiflePrefab;
    [SerializeField] private GameObject _shotgunPrefab;
    [SerializeField] private GameObject _medkitPrefab;

    [SerializeField] private GameObject _pistolAmmoPrefab;
    [SerializeField] private GameObject _automaticRifleAmmoPrefab;
    [SerializeField] private GameObject _shotgunAmmoPrefab;

    [SerializeField] private List<GameObject> _guns = new List<GameObject>();
    [SerializeField] private List<GameObject> _ammo = new List<GameObject>();
    [SerializeField] private List<GameObject> _medkit = new List<GameObject>();

    public virtual void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _guns.Add(_pistolPrefab);
        _guns.Add(_automaticRiflePrefab);
        _guns.Add(_shotgunPrefab);

        _medkit.Add(_medkitPrefab);

        _ammo.Add(_pistolAmmoPrefab);
        _ammo.Add(_automaticRifleAmmoPrefab);
        _ammo.Add(_shotgunAmmoPrefab);

        int amount = Random.Range(3, 6);

        SpawnPickups(amount, _guns, _medkit, _ammo);
    }

    public void SpawnPickups(int count, List<GameObject> guns, List<GameObject> medkit, List<GameObject> ammo)
    {
        float randomX;
        float randomY;
        Vector3 position = new Vector3();
        position.z = 0;

        int rate;

        GameObject chosenPrefab;

        for (int i = 0; i < count; i++)
        {
            randomX = Random.Range(-3, 3);
            randomY = Random.Range(-3, 3);
            position.x = gameObject.transform.position.x + randomX;
            position.y = gameObject.transform.position.y + randomY;

            rate = Random.Range(0, 10);
            
            if (rate == 0)
            {
                chosenPrefab = guns[Random.Range(0, guns.Count)];
            }
            else if (rate >= 1 && rate <= 3)
            {
                chosenPrefab = medkit[Random.Range(0, medkit.Count)];
            }
            else
            {
                chosenPrefab = ammo[Random.Range(0, ammo.Count)];
            }

            GameObject pickupGO = Instantiate(chosenPrefab, position, Quaternion.identity);
            pickupGO.transform.parent = transform;

        }
    }
}
