using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : Singleton<Sound>
{
    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip pickupItem;
    public AudioClip hurtEnemy;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PickupItem()
    {
        audioSource.PlayOneShot(pickupItem);
    }

    public void HurtEnemy()
    {
        audioSource.PlayOneShot(hurtEnemy);
    }
}