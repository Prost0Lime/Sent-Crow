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
        anim.SetTrigger("Fade");
        position = playerStorage.initialValue;              //приём сохранённых координат для перемещения
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

        if (SceneMenu != 0)         //чтобы не сохранялись нулевые коорды при выходе в меню
        {
            playerStorage.initialValue = position;
        }
            SceneManager.LoadScene(SceneMenu);
    }
}
