using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimation : MonoBehaviour, CarAnimationBase
{
    public Animator carAnim;

    private void Start()
    {
        carAnim = GetComponent<Animator>();
    }
    public void CarTurnLeft()
    {
        carAnim.SetBool("TurnLeft", true);
    }

    public void CarTurnRight()
    {
        carAnim.SetBool("TurnRight", true);
    }

    public void CarNoTurn()
    {
        carAnim.SetBool("TurnRight", false);
        carAnim.SetBool("TurnLeft", false);
    }
}
