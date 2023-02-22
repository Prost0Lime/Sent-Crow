using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SettM;
    public Animator settAnim;
    private bool OpenTr;
    public Camera cm;

    private void Start()
    {

        SettM.SetActive(false);
        OpenTr = false;
        cm = GameObject.FindGameObjectWithTag("UI Camera").GetComponent<Camera>();
    }

    [System.Obsolete]
    public void SettingsOpen()
    {
        
        if (OpenTr == false)        //��������
        {
            cm.orthographicSize = 0.1f;
            OpenTr = true;
            settAnim.SetBool("AnimTrgg", true);
            SettM.SetActive(true);
            
        }
        else if (OpenTr == true)    //��������
        {
            cm.orthographicSize = 5f;
            OpenTr = false;
            settAnim.SetBool("AnimTrgg", false);
            SettM.SetActive(false);
  

        }
    }
}

