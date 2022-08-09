using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private Animator Anim;
    
    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        Anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
        if (destory_anim) 
        {
            //Debug.Log("anim!");
            Anim.SetTrigger("Death");
        }
    }

}
