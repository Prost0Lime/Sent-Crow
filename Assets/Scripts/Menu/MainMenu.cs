using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator anim;
    public int SceneMenu;
    public Vector3 position;
    public VectorValue playerStorage;
    public SaveMethod SM;
    private bool goToMenu;


    private void Start()
    {
        anim = GetComponent<Animator>();
        SM = GameObject.FindGameObjectWithTag("SaveMethod").GetComponent<SaveMethod>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SM.LoadSettings();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (SM.LoadComplite == true)
            {
                anim.speed = 1;
            }
            else
            {
                anim.speed = 0;
            }
        }
    }

    public void goToMainMenu()
    {
        goToMenu = true;        
        anim.SetTrigger("Fade");
        SceneMenu = 0;
        Time.timeScale = 1f;                        //разморозка игры
        
    }

    public void NewGame()
    {
        anim.SetTrigger("Fade");
        SceneMenu = 1;
        position = new Vector3(-3.86f, 9.23f, 0f);      //начальные координаты игрока при сбросе игры
        SM.ResetData();
    }

    public void LoadGame()
    {

        position = playerStorage.initialValue;              //приЄм сохранЄнных координат дл€ 
        anim.SetTrigger("Fade");
    }

    public void doExitGame()
    {
        Application.Quit();
    }

    public void OnFadeComplete()
    {
        if (goToMenu == true)
        {
            goToMenu = false;
            SM.SaveGame();
        }

        if (SceneMenu != 0)         //чтобы не сохран€лись нулевые коорды при выходе в меню
        {
            playerStorage.initialValue = position;
        }
            SceneManager.LoadScene(SceneMenu);
    }

}
