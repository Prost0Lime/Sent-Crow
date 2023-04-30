using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SettM;
    public Animator settAnim;
    private bool OpenTr;
    public Camera UIcm;
    public ParticleSystem PS;

    [HideInInspector]
    public SaveMethod SM;
    [HideInInspector]
    public SoundManager soundManager;

    private void Start()
    {
        SettM.SetActive(false);
        OpenTr = false;
        UIcm = GameObject.FindGameObjectWithTag("UI Camera").GetComponent<Camera>();
        SM = GameObject.FindGameObjectWithTag("SaveMethod").GetComponent<SaveMethod>();
        soundManager = GameObject.FindGameObjectWithTag("AM").GetComponent<SoundManager>();
        soundManager.AudioSound.clip = soundManager.Shesternya;
        soundManager.AudioSound.Play();
    }

    public void SettingsOpen()
    {
        
        if (OpenTr == false)        //��������
        {
           // UIcm.orthographicSize = 0.1f;
            OpenTr = true;
            settAnim.SetBool("AnimTrgg", true);
            SettM.SetActive(true);
            Invoke("ParticleOff", 1f);



        }
        else if (OpenTr == true)    //��������
        {
            // UIcm.orthographicSize = 5f;
            OpenTr = false;
            settAnim.SetBool("AnimTrgg", false);
            SettM.SetActive(false);
            SM.SaveSettings();
            PS.Play();
  

        }
    }

    public void ParticleOff()
    {
        PS.Clear();
        PS.Pause();
    }
}

