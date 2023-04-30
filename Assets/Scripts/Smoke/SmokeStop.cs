using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeStop : MonoBehaviour
{
    public ParticleSystem smoke;
    public bool Smoke_Start;
    public bool Smoke_Stop;
    public GameObject selfSmokeController;
    public GameObject Sound;
    public bool UseSound;
    public int TimeToSound;
    
    private void Start()
    {
        smoke.Pause();
        smoke.Clear();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Smoke_Stop == true)
            {
                smoke.emissionRate = 0f;
                Invoke("TimeOff", 1f);
            }
        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Smoke_Start == true)
            {
                smoke.emissionRate = 10f;
                smoke.Play();
                if (UseSound == true)
                {
                    Invoke("SoundOn", TimeToSound);
                }
                Invoke("TimeOff", 1f);
            }
        }
    }
    public void TimeOff()
    {
        selfSmokeController.SetActive(false);
    }
    
    public void SoundOn()
    {
        Sound.SetActive(true);
    }
}
