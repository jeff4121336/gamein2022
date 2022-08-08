using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerattack : MonoBehaviour
{
    [SerializeField] [Range(0, 20f)] private int damage;
    [SerializeField] [Range(0, 1f)] private float hitbox_time;
    [SerializeField] [Range(0, 1f)] private float starthitbox_time;
    [SerializeField] [Range(0, 5f)] private float attack_cd;
    [SerializeField] private bool Attack_Enable = true;

    [SerializeField] private PolygonCollider2D PolyCol2D;
    [SerializeField] private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        PolyCol2D = GetComponent<PolygonCollider2D>(); 
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack() 
    {
        if (Input.GetKey(KeyCode.A) && Attack_Enable) {
            StartCoroutine(starthitbox());
            anim.SetTrigger("Attack");
            StartCoroutine(disablehitbox());
            Attack_Enable = false;
            StartCoroutine(attackCD());
        }
        
    }

    IEnumerator starthitbox() 
    {
        yield return new WaitForSeconds(starthitbox_time);
        PolyCol2D.enabled = true;
    }

    IEnumerator disablehitbox()
    {
        yield return new WaitForSeconds(hitbox_time);
        PolyCol2D.enabled = false;
    }

    IEnumerator attackCD()
    {
        yield return new WaitForSeconds(attack_cd);
        Attack_Enable = true;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Enemy")) 
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
