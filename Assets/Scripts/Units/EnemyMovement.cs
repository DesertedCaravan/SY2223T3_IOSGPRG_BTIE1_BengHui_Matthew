using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Reference: https://discussions.unity.com/t/make-sprite-look-at-vector2-in-unity-2d/97929

    // Get Enemy unit._speed
    [SerializeField] private Unit _unit;

    [SerializeField] public GameObject _playerTarget;
    [SerializeField] public EnemyMovement _currentTarget;

    Vector3 _defaultAim;

    public bool _deathState = false;
    public bool _aimSwitch = true;

    private float _xRotation;
    private float _rotationChangeElapse;

    private bool _spottedPlayer = false;
    private bool _spottedEnemy = false;
    private float _trackingSpeed = 3.0f;

    private void Start()
    {
        _defaultAim = _playerTarget.transform.position;

        _trackingSpeed = Random.Range(2.5f, 3.5f);

        StartCoroutine(CO_ChangeDefaultAim());
    }

    private void LateUpdate()
    {
        if (_deathState == false)
        {
            if ((_spottedPlayer == false && _spottedEnemy == false) && _aimSwitch == true)
            {
                _xRotation = Random.Range(0, 361);

                transform.rotation = Quaternion.AngleAxis(_xRotation, Vector3.forward);

                _aimSwitch = false;
                StartCoroutine(CO_ChangeAim());
            }

            if (_spottedPlayer == true)
            {
                Vector3 _diff = (_playerTarget.transform.position - transform.position);
                float atan2 = Mathf.Atan2(_diff.y, _diff.x);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg - 90f), _trackingSpeed * Time.deltaTime);

                if (InRange(1, 7.5f) == true)
                {
                    _unit.Fire();
                }
            }
            else if (_spottedEnemy == true)
            {
                Vector3 _diff;

                if (_currentTarget != null)
                {
                    _diff = (_currentTarget.transform.position - transform.position);
                }
                else
                {
                    _diff = (_defaultAim - this.transform.position);
                }

                float atan2 = Mathf.Atan2(_diff.y, _diff.x);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg - 90f), _trackingSpeed * Time.deltaTime);

                if (_currentTarget != null && InRange(2, 7.5f) == true)
                {
                    _unit.Fire();
                }
            }

            if (InRange(1, 10f) == true || InRange(2, 7.5f) == false)
            {
                transform.position += transform.up * _unit._speed * Time.deltaTime;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D trigger)
    {
        PlayerMovement player = trigger.gameObject.GetComponent<PlayerMovement>();
        EnemyMovement enemy = trigger.gameObject.GetComponent<EnemyMovement>();

        if (player != null)
        {
            _spottedPlayer = true;
        }
        
        if (enemy != null)
        {
            _spottedEnemy = true;
            _currentTarget = enemy;
        }
    }

    public void OnTriggerExit2D(Collider2D trigger)
    {
        PlayerMovement player = trigger.gameObject.GetComponent<PlayerMovement>();
        EnemyMovement enemy = trigger.gameObject.GetComponent<EnemyMovement>();

        if (player != null)
        {
            _spottedPlayer = false;
        }

        if (enemy != null)
        {
            _spottedEnemy = false;
            _currentTarget = null;
        }
    }

    public bool InRange(int unit, float range)
    {
        Vector3 _playerDistance = _playerTarget.transform.position - this.transform.position;
        Vector3 _enemyDistance;

        if (_currentTarget != null)
        {
            _enemyDistance = _currentTarget.transform.position - this.transform.position;
        }
        else
        {
            _enemyDistance = _defaultAim - this.transform.position;
        }

        if (_playerDistance.magnitude < range && unit == 1)
        {
            return true;
        }
        else if (_enemyDistance.magnitude < range && unit == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CallDeath()
    {
        _deathState = true;
    }

    IEnumerator CO_ChangeAim()
    {
        _rotationChangeElapse = Random.Range(0f, 2f);

        yield return new WaitForSecondsRealtime(_rotationChangeElapse);
        _aimSwitch = true;
    }

    IEnumerator CO_ChangeDefaultAim()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(10f);

            float xPos = Random.Range(-100f, 100f);
            float yPos = Random.Range(-50, 50);

            _defaultAim = new Vector3(xPos, yPos, 0f);
        }
    }
}