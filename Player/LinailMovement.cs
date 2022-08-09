using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    *** Key that need to insert -> RUN in unity
    *** DashAbility.cs inherited to this (suppose..)
*/

public class LinailMovement : MonoBehaviour
{
    protected float walkspeed = 5.55f;
    protected float runspeed = 8.5f;
    float horizontal_initial;

    protected bool jump = false;
    protected bool run = false;

    public CharacterController2D controller;
    private float Running_Using_Energy_Time = 0.05f;

    public Slider EnergySlider;

    void Update()
    {
        //Debug.Log(run);
        horizontal_initial = Input.GetAxisRaw("Horizontal") * walkspeed;

        if (Input.GetKey(KeyCode.R)) 
        {
            horizontal_initial = Input.GetAxisRaw("Horizontal") * runspeed;
            run = true;
        } 

        if (Input.GetKeyUp(KeyCode.R)) 
        {
            run = false;
        } 

        if (Input.GetButtonDown("Jump"))
        {
            horizontal_initial = 0;
            jump = true;
        }

    }

    void FixedUpdate() 
    {

            if (EnergySlider.value > 0 && run)
            {
                StartCoroutine("RunningEnergy");
            } 
            else
            {
                run = false;
                StartCoroutine("RunningEnergy");
            }

        //Debug.Log(horizontal_initial * Time.fixedDeltaTime);
        controller.Move(horizontal_initial * Time.fixedDeltaTime, jump);
        jump = false;

    }

    IEnumerator RunningEnergy() 
    {
        yield return new WaitForSeconds(Running_Using_Energy_Time);
        if (run)
        {
            EnergySlider.value -= 0.6f;
        } else 
        {
            EnergySlider.value += 0.15f;
        }
    }
}
