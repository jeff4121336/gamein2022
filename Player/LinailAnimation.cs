using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinailAnimation : LinailMovement
{
    [SerializeField] protected Animator Linail_Animation;
    protected bool grounded;
    public CharacterController2D CC2DAnimation;
    
    void Start() 
    {
        Linail_Animation = GetComponent<Animator>();
    }

    void Update() 
    {
        grounded = CC2DAnimation.m_Grounded;
        Linail_Animation.SetBool("LinailJump", false);
        if (Input.GetKey(KeyCode.Space) && grounded == false)
        {
            Linail_Animation.SetBool("LinailJump", true);
            //Debug.Log("Animation-Jump Trigger!");
        }

        //if (Input.GetKey(KeyCode.A))
        //{
        //    Linail_Animation.SetBool("LinailAttackOne", true);
            //Debug.Log("Animation-AttackOne Trigger!");
        //} 

        if (grounded) 
        {
            
            Linail_Animation.SetBool("LinailStandStill", true);
            //Debug.Log("Animation-StandStill Trigger!");

            if (Input.GetAxisRaw("Horizontal") != 0) 
            {
                Linail_Animation.SetFloat("LinailRunning", .2f);
                //Debug.Log("Animation-Running Trigger!");
            } else
            {
            Linail_Animation.SetFloat("LinailRunning", .0f);
            //Debug.Log("Animation-Running Reset!");
            }

        } else {
            Linail_Animation.SetBool("LinailStandStill", false);
            //Debug.Log("Animation-StandStill Reset!");
        }
    }        
}
