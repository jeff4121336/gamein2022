using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinailHealthMana : MonoBehaviour
{
    [SerializeField] [Range(0, 100f)] public float default_health;
    [SerializeField] [Range(0, 100f)] public float default_mana;
    public Slider Mana;
    public Slider Health;
    public float RegenerateMana_AMT;
    public float RegenerateHealth_AMT;
    public float RegenerationMana_SPD;
    public float RegenerationHealth_SPD;

    private ScreenFlash sf;

    void Start() 
    {
        Health.value = default_health;
        Mana.value = default_mana;
        sf = GetComponent<ScreenFlash>();
    }
    // Update is called once per frame
    void Update()
    {   
        if (Mana.value < 100) 
        {
            StartCoroutine("RegenerationTimer_MANA"); 
        }    

        if (Health.value < 100 && Health.value > 0) 
        {
            StartCoroutine("RegenerationTimer_HEALTH"); 
        }   
    }

    IEnumerator RegenerationTimer_MANA()
    {
        yield return new WaitForSeconds(RegenerationMana_SPD);
        Mana.value += RegenerateMana_AMT;
    }

    
    IEnumerator RegenerationTimer_HEALTH()
    {
        yield return new WaitForSeconds(RegenerationHealth_SPD);
        Health.value += RegenerateHealth_AMT;
    }

    public void DamagePlayer(int damage) 
    {
        sf.screenflash();
        Health.value -= damage;
        if (Health.value <= 0) 
        {
            Destroy(gameObject);
        }
    }


}
