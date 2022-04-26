using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterDisplay : MonoBehaviour
{
    public KarlController karl;
    public GameObject DisplayComponent;
    public bool latterOn;
    private bool animOn; // чтобы не спамить кнопкой открыть и закрыть

    public Animator backgroundAnim;
    public Animator papperAnim;
    void Start()
    {
        DisplayComponent.SetActive(false);
        karl = GameObject.FindGameObjectWithTag("Player").GetComponent<KarlController>();
        animOn = false;
    }

    public void DisplayLetter()
    {
        if ((latterOn == false) && (animOn == false))
        {
            animOn = true;
            latterOn = true;
            DisplayComponent.SetActive(true);
            backgroundAnim.SetBool("BackgroundAnim", true);
            papperAnim.SetBool("PapperAnim", true);
            
        }
        else if ((latterOn == true) && (animOn == true))
        {
            latterOn = false;
            backgroundAnim.SetBool("BackgroundAnim", false);
            papperAnim.SetBool("PapperAnim", false);
            Invoke("Active", 1f);
            karl.CheckOff(); //анимация письма
        }
    }

    public void Active()
    {
        DisplayComponent.SetActive(false); 
        animOn = false; // чтобы не спамить кнопкой открыть и закрыть
    }

}
