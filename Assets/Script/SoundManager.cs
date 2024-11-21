using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource_laser;
    public AudioSource audioSource_circle;
    public AudioSource audioSource_background;

    [SerializeField] private AudioClip laserSound, boomSound, backGroundSound;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource_background.clip = backGroundSound;
        audioSource_background.Play();
    }

    public void LaserSound(){
        audioSource_laser.clip = laserSound;
        audioSource_laser.Play();
    }
    public void BoomSound(){
        audioSource_circle.clip = boomSound;
        audioSource_circle.Play();
    }
}
