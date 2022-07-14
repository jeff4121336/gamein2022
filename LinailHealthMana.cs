using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinailHealthMana : MonoBehaviour
{

    public Slider RegenerateMana;
    public Slider RegenerateHealth;
    public float RegenerateMana_AMT;
    public float RegenerateHealth_AMT;
    public float RegenerationMana_SPD;
    public float RegenerationHealth_SPD;

    // Update is called once per frame
    void Update()
    {
        if (RegenerateMana.value < 100) 
        {
            StartCoroutine("RegenerationTimer_MANA"); 
        }    

        if (RegenerateHealth.value != 100) 
        {
            Debug.Log("Regenrate needed");
        }   
    }

    IEnumerator RegenerationTimer_MANA()
    {
        yield return new WaitForSeconds(RegenerationMana_SPD);
        RegenerateMana.value += RegenerateMana_AMT;
    }
}
