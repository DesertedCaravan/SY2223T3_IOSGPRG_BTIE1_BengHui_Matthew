using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] public float speed = 3.0f;

    void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;

        Destroy(gameObject, 7.0f);
    }
}