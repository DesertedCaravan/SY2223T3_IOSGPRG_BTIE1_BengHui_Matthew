using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;

    // [SerializeField] private int _zoom;

    private void Update()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y, _player.position.z - 10);
    }
}
