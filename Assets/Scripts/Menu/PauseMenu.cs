using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool pauseOn;
    public GameObject blur;
    public GameObject MainMuneBttn;
    public GameObject ResetBttn;

    private void Start()
    {
        blur.SetActive(false);
        MainMuneBttn.SetActive(false);
    }

    public void Pause()
    {
        if (pauseOn == false)
        {
            pauseOn = true;
            Time.timeScale = 0f;
            blur.SetActive(true);
            MainMuneBttn.SetActive(true);
            ResetBttn.SetActive(true);

        }
        else if (pauseOn == true)
        {
            pauseOn = false;
            Time.timeScale = 1f;
            blur.SetActive(false);
            MainMuneBttn.SetActive(false);
            ResetBttn.SetActive(false);
        }
    }
    
}
