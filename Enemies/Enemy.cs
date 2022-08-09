using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] [Range(0, 5f)] private float DAD_time; //Dead Animation Delay time
    [SerializeField] private Animator anim; //DEAD ANIMATION NOT INCLUDED HERE

    public float health;
    public float damage;
    public float flashtime;

    private SpriteRenderer SR;
    private Color OriginalColor;
    public GameObject bloodeffect;
    public bool destory_anim;

    private LinailHealthMana LinailHealth;

    public void Start() 
    {   
        LinailHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<LinailHealthMana>();
        destory_anim = false;
        SR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        OriginalColor = SR.color;
    }

    public void Update()
    {
        if (health <= 0) 
        {   
            StartCoroutine(DeadAnimationDelay());
            damage = 0;
            destory_anim = true;
        }
    }

    public void TakeDamage(float damage) 
    {
        health -= damage;
        anim.SetTrigger("Hit");
        if (health >= 0) 
        {
            DamageTakenShows(flashtime);
            Instantiate(bloodeffect, transform.position, Quaternion.identity);
        }
    }

    void DamageTakenShows(float time) 
    {
        SR.color = Color.red;
        Invoke("Reset", time);
    }

    void Reset() 
    {
        SR.color = OriginalColor;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player") && destory_anim == false)
        {
            if (LinailHealth != null) 
            {   
                anim.SetTrigger("Attack");
                LinailHealth.DamagePlayer(damage);
            }
        }
    }

    IEnumerator DeadAnimationDelay()
    {
        yield return new WaitForSeconds(DAD_time);
        Destroy(gameObject);
    }


}
