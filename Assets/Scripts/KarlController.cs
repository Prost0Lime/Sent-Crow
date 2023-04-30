using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KarlController : MonoBehaviour
{
    public Joystick joystick;

    public float speed = 3.0f;  //скорость перемещения

    Rigidbody2D rigidbody2d;
    private float horizontal;
    private float vertical;

    public float hor;       //переменные для обработки lookdirection.x
    public float ver;       //переменные для обработки lookdirection.y

    Animator animator;
    Vector2 lookDirection = new Vector2(0, -1);
    public VectorValue pos;     //для кординат игрока

    [HideInInspector]
    public SoundManager soundManager;


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        transform.position = pos.initialValue;
        soundManager = GameObject.FindGameObjectWithTag("AM").GetComponent<SoundManager>();
    }

    void Update()
    {
        horizontal = joystick.Horizontal;       //джойстик
        vertical = joystick.Vertical;

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        hor = Mathf.Round(lookDirection.x);     //округление lookDirection.x
        ver = Mathf.Round(lookDirection.y);     //округление lookDirection.y

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void CheckOn()
    {
        animator.SetBool("Check", true);
        soundManager.AudioSound2.clip = soundManager.Letter;
        soundManager.AudioSound2.Play();
    }
    
    public void CheckOff()
    { 
        animator.SetBool("Check", false);
    }

    public void LampOn()
    {
        animator.SetBool("Lamp", true);
        soundManager.AudioSound2.clip = soundManager.Lamp1;
        soundManager.AudioSound2.Play();
    }

    public void LampOff()
    {
        animator.SetBool("Lamp", false);
        soundManager.AudioSound2.clip = soundManager.Lamp2;
        soundManager.AudioSound2.Play();
    }

    public void Step()
    {
        soundManager.AudioSound.clip = soundManager.Steps;
        soundManager.AudioSound.Play();
    }
}
