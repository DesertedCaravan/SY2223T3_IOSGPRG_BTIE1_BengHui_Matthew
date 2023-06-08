using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : Singleton<EnvironmentSpawner>
{
    [SerializeField] private GameObject windowObject;
    [SerializeField] private GameObject cloudObject;
    [SerializeField] private Transform spawnedParent;
    [SerializeField] private List<GameObject> windows;

    private Vector3 windowPosition;
    private Vector3 cloudPosition;

    private bool _gameOn = false;
    private bool _coroutine = false;

    float _xPosCloud;
    bool _superModeWindow = false;
    bool _superModeWindowSetUp = false;
    float _windowSpawnRate = 1.5f;
    float _cloudSpawnRate = 2.5f;

    void Update()
    {
        if (_gameOn == true)
        {
            StartCoroutine(CO_Window());
            StartCoroutine(CO_Cloud());

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

    public void RemoveWindowFromList(GameObject item)
    {
        windows.Remove(item);
    }

    public void ActivateSuperMode()
    {
        _superModeWindow = true;
        
        _windowSpawnRate = 0.375f;
    }

    public void DeactivateSuperMode()
    {
        _superModeWindowSetUp = false;

        _windowSpawnRate = 1.5f;
    }

    private IEnumerator CO_Window()
    {
        while (_coroutine)
        {
            yield return new WaitForSeconds(_windowSpawnRate);

            if (_superModeWindow == true && _superModeWindowSetUp == false)
            {
                for (int j = 0; j < windows.Count; j++)
                {
                    Window previousWindows = windows[j].GetComponent<Window>();
                    previousWindows.ActivateSuperMode();
                }

                _superModeWindow = false;
                _superModeWindowSetUp = true;
            }

            windowPosition = new Vector3(1.2f, 6.0f, -0.5f);
            GameObject window = Instantiate(windowObject, windowPosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));

            window.transform.parent = spawnedParent;

            windows.Add(window);

            if (_superModeWindowSetUp == true)
            {
                Window windowScript = window.GetComponent<Window>();
                windowScript.ActivateSuperMode();
            }
        }
    }

    private IEnumerator CO_Cloud()
    {
        while (_coroutine)
        {
            yield return new WaitForSeconds(_cloudSpawnRate);

            _xPosCloud = Random.Range(-1.0f, -2.6f);
            cloudPosition = new Vector3(_xPosCloud, 6.0f, 0.25f);
            GameObject cloud = Instantiate(cloudObject, cloudPosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));

            cloud.transform.parent = spawnedParent;
        }
    }
}