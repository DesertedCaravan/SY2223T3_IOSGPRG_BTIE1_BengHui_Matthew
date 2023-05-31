using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject cloud;

    private Vector3 windowPosition;
    private Vector3 cloudPosition;

    // float timer1;
    // float timer2;

    float _xPosCloud;

    private void Start()
    {
        // spawnWindow();
        StartCoroutine(CO_Window());
        StartCoroutine(CO_Cloud());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator CO_Window()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);

            windowPosition = new Vector3(1.2f, 6.0f, -0.5f);
            Instantiate(window, windowPosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
    }
    private IEnumerator CO_Cloud()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);

            _xPosCloud = Random.Range(-1.0f, -2.6f);
            cloudPosition = new Vector3(_xPosCloud, 6.0f, 0.25f);
            Instantiate(cloud, cloudPosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
    }
}

// Unused Code
/*
    timer1 += Time.deltaTime;
    timer2 += Time.deltaTime;

    if (timer1 > 1.5f)
    {
        spawnWindow();
        timer1 = 0;
     }

    if (timer2 > 2.5f)
     {
        spawnCloud();
        timer2 = 0;
     }
--------------------------------------------------------------------------------------

private void spawnWindow()
    {
        windowPosition = new Vector3(1.2f, 6.0f, -0.5f);

        Instantiate(window, windowPosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));
    }

    private void spawnCloud()
    {
        xPosCloud = Random.Range(-1.0f, -2.6f);

        cloudPosition = new Vector3(xPosCloud, 6.0f, 0.25f);

        Instantiate(cloud, cloudPosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));
    }
 */