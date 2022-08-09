using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinailHealthMana : MonoBehaviour
{
    [SerializeField] [Range(0, 200f)] public float default_health;
    [SerializeField] [Range(0, 200f)] public float default_mana;
    
    public Slider Mana;
    public Slider Health;
    [SerializeField] [Range(0, 5f)] public float health_increase;
    [SerializeField] [Range(0, 5f)] public float mana_increase;
    [SerializeField] [Range(0, 10f)] public float regen_cd;

    private ScreenFlash sf;

    void Start() 
    {
        Health.maxValue = default_health;
        Mana.maxValue = default_mana;
        sf = GetComponent<ScreenFlash>();
    }
    // Update is called once per frame
    void Update()
    {   
        if (Mana.value < default_mana) 
        {
            StartCoroutine(regen_timer());
            Mana.value += mana_increase * Time.deltaTime;
        }    

        if (Health.value < default_health && Health.value > 0) 
        {
            StartCoroutine(regen_timer());
            Health.value += health_increase * Time.deltaTime;
        }   
    }

    public void DamagePlayer(float damage) 
    {
        sf.screenflash();
        Debug.Log(damage);
        Health.value -= damage * 0.5f; //COLLIDERS TRIGGER TWICE A TIME
        if (Health.value <= 0) 
        {
            Destroy(gameObject);
        }
    }

    IEnumerator regen_timer() 
    {
        yield return new WaitForSeconds(regen_cd);
    }
}
