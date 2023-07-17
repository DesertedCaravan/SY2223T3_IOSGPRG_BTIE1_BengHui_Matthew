using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private void Start()
    {
        Initialize("Player", 100, 5);
    }
}

/*
private void Update()
{
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
        Shoot();
    }
}
*/