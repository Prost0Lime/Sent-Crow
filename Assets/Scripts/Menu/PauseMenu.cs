using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool pauseOn;
    public GameObject blur;
    public GameObject MainMenuBttn;
    public GameObject Volume;
    [HideInInspector]
    public SoundManager soundManager;
    public AudioSource[] audioSources;

    private void Start()
    {
        blur.SetActive(false);
        MainMenuBttn.SetActive(false);
        soundManager = GameObject.FindGameObjectWithTag("AM").GetComponent<SoundManager>();
    }

    public void Pause()
    {
        if (pauseOn == false)
        {
            pauseOn = true;
            Time.timeScale = 0f;
            blur.SetActive(true);
            MainMenuBttn.SetActive(true);
            Volume.SetActive(true);
            soundManager.AudioSound2.clip = soundManager.PauseButton;
            soundManager.AudioSound2.Play();
            soundManager.AudioTheme.Pause();
            for (int i=0; i < audioSources.Length; i++)
            {
                audioSources[i].Pause();
            }


        }
        else if (pauseOn == true)
        {
            pauseOn = false;
            Time.timeScale = 1f;
            blur.SetActive(false);
            MainMenuBttn.SetActive(false);
            Volume.SetActive(false);
            soundManager.AudioSound2.clip = soundManager.PauseButton;
            soundManager.AudioSound2.Play();
            soundManager.AudioTheme.Play();
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].Play();
            }
        }
    }
    
}
