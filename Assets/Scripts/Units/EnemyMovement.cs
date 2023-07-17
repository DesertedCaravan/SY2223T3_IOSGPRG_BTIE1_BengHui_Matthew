using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Get Enemy unit._speed
    [SerializeField] private Unit unit;
    private Rigidbody2D rb;

    [SerializeField] public Unit target;

    // private float _rotationSpeed;

    public bool _deathState = false;
    public bool _aimSwitch = true;

    private float _xRotation;
    private float _rotationChange;

    private int _fireOn;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // _rotationSpeed = 5f;

        StartCoroutine(FireAway());
    }

    private void LateUpdate()
    {
        if (_deathState == false)
        {
            /*
            Debug.Log($"Target in Range: {InRange()}");
            // Debug.Log($"Target Spotted: {CanSeeTarget()}");

            if (InRange() == true) //  && CanSeeTarget() == true // NEEDS FIXING
            {
                // Reference: https://discussions.unity.com/t/rotate-rigidbody2d-towards-target/126125/2

                Quaternion toRotation = Quaternion.FromToRotation(target.transform.position, this.transform.position);
                this.transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 5f);
                
                Debug.Log("In Range");
            }
            else
            {
            }
            */

            if (_aimSwitch == true)
            {
                _xRotation = Random.Range(0, 361);

                rb.SetRotation(_xRotation);

                _aimSwitch = false;
                StartCoroutine(ChangeAim());

                Debug.Log("Firing");
            }

            transform.position += transform.up * unit._speed * Time.deltaTime;
        }
    }

    public bool InRange() // NEEDS FIXING
    {
        Vector3 distance = target.transform.position - this.transform.position;

        if (distance.magnitude < 50f) // ensures that the object stops moving up until a certain point.
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanSeeTarget() // NEEDS FIXING
    {
        // shoots a raycast from the target
        RaycastHit raycastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;

        // if the raycast directly hits a "Player" tag, return true
        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            return raycastInfo.transform.gameObject.tag == "Player";
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

    IEnumerator ChangeAim()
    {
        _rotationChange = Random.Range(0f, 2f);

        yield return new WaitForSecondsRealtime(_rotationChange);
        _aimSwitch = true;
    }

    IEnumerator FireAway()
    {
        while(true)
        {
            _fireOn = Random.Range(1, 6);

            yield return new WaitForSecondsRealtime(_fireOn);

            _fireOn = Random.Range(1, 15);

            for (int i = 0; i < _fireOn; i++)
            {
                unit.Fire();
            }
        }
    }
}

/*
    public bool _directionSwitch = true;

    if (_directionSwitch == true)
    {

        _xMovement = Random.Range(-1, 2);
        _yMovement = Random.Range(-1, 2);

        rb.velocity = new Vector2(_xMovement * unit._speed, _yMovement * unit._speed);

        _directionSwitch = false;
        StartCoroutine(ChangeDirection());
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        _directionSwitch = true;
    }
*/