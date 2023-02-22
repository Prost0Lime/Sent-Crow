using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicThemeChange : MonoBehaviour
{
    public AudioClip[] MusicTheme;
    public AudioSource Music;
    private int n; //��� ���������� ������ ����� � ��������� ����������
    private bool Defend;    //������ �� ������ �������

    public Animator VynilAnim;

    private void Start()
    {
        Music.clip = MusicTheme[0];
        Music.pitch = 1f;
        Music.Play();
        Defend = false;
    }

    public void ChangeMusicTheme()
    {
        if (Defend == false)
        {
            Defend = true;
            VynilAnim.SetTrigger("Change");

            for (int i = 0; i < MusicTheme.Length; i++)
            {
                if (Music.clip == MusicTheme[i])
                {
                    if (i == MusicTheme.Length - 1)
                    {
                        i = 0;
                        n = i;

                        Music.clip = MusicTheme[n]; //������ ����
                        Invoke("PlayM", 3f); //���� 3 ���
                    }
                    else
                    {
                        i++;
                        n = i;

                        Music.clip = MusicTheme[n];
                        Invoke("PlayM", 3f);
                    }
                }
            }
        }
    }

    void PlayM()
    {
        Music.Play();
        Defend = false;
    }
    
}