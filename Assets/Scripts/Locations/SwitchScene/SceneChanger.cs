using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public SaveMethod SM;           //������ ����������
    private Animator anim;
    public int sceneToLoad;

    public Vector3 position;
    public VectorValue playerStorage;

    private void Start()
    {
        anim = GetComponent<Animator>();
        SM = GameObject.FindGameObjectWithTag("SaveMethod").GetComponent<SaveMethod>();
    }

    public void FadeToScene()
    {
        anim.SetTrigger("Fade");
    }

    public void OnFadeComplete()
    {
        SM.SaveGame();              //���������� ������ ��� ����������� �� ������ �����
        playerStorage.initialValue = position;
       
        SceneManager.LoadScene(sceneToLoad);
    }
}
