using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Reference: https://discussions.unity.com/t/make-sprite-look-at-vector2-in-unity-2d/97929

    // Get Enemy unit._speed
    [SerializeField] private Unit unit;

    [SerializeField] public GameObject _playerTarget;
    [SerializeField] public GameObject _enemyTarget;

    public bool _deathState = false;
    public bool _aimSwitch = true;

    private float _xRotation;
    private float _rotationChangeElapse;

    private bool _spotted = false;
    private float _trackingSpeed = 2.0f;

    private void LateUpdate()
    {
        if (_deathState == false)
        {
            if (_spotted == false && _aimSwitch == true)
            {
                _xRotation = Random.Range(0, 361);

                transform.rotation = Quaternion.AngleAxis(_xRotation, Vector3.forward);

                _aimSwitch = false;
                StartCoroutine(CO_ChangeAim());
            }

            if (_spotted == true)
            {
                Vector3 v_diff = (_playerTarget.transform.position - transform.position);
                float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg - 90f), _trackingSpeed * Time.deltaTime);

                if (InRange())
                {
                    unit.Fire();
                }
            }

            transform.position += transform.up * unit._speed * Time.deltaTime; 
        }
    }

    public void OnTriggerEnter2D(Collider2D trigger)
    {
        PlayerMovement player = trigger.gameObject.GetComponent<PlayerMovement>();
        EnemyMovement enemy = trigger.gameObject.GetComponent<EnemyMovement>();

        if (player != null)
        {
            _spotted = true;
        }
    }

    public void OnTriggerExit2D(Collider2D trigger)
    {
        PlayerMovement player = trigger.gameObject.GetComponent<PlayerMovement>();
        EnemyMovement enemy = trigger.gameObject.GetComponent<EnemyMovement>();

        if (player != null)
        {
            _spotted = false;
        }
    }

    public bool InRange()
    {
        Vector3 distance = _playerTarget.transform.position - this.transform.position;

        if (distance.magnitude < 7.5f) // ensures that the object stops moving up until a certain point.
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
}