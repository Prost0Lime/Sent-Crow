using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool pauseOn;
    public GameObject blur;
    public GameObject MainMenuBttn;
    public GameObject ResetBttn;
    public GameObject Volume;

    private void Start()
    {
        blur.SetActive(false);
        MainMenuBttn.SetActive(false);
    }

    public void Pause()
    {
        if (pauseOn == false)
        {
            pauseOn = true;
            Time.timeScale = 0f;
            blur.SetActive(true);
            MainMenuBttn.SetActive(true);
            ResetBttn.SetActive(true);
            Volume.SetActive(true);

        }
        else if (pauseOn == true)
        {
            pauseOn = false;
            Time.timeScale = 1f;
            blur.SetActive(false);
            MainMenuBttn.SetActive(false);
            ResetBttn.SetActive(false);
            Volume.SetActive(false);
        }
    }
    
}
