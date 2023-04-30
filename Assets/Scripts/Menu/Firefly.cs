using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public Animator anim;
    [HideInInspector]
    public SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("AM").GetComponent<SoundManager>();
    }

    public void SeatTrigger()
    {
        anim.SetTrigger("SeatTr");
    }

    public void FlyAudio()
    {
        soundManager.AudioSound2.clip = soundManager.Firefly;
        soundManager.AudioSound2.Play();
    }
}
