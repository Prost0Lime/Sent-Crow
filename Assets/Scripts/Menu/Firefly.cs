using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public Animator anim;

   public void SeatTrigger()
    {
        anim.SetTrigger("SeatTr");

    }
}
