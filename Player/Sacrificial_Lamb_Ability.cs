using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sacrificial_Lamb_Ability : MonoBehaviour
{

    public GameObject playeruse;
    public AbilityScriptableObject ability_two;
    public Slider ManaSlider;
    private bool abilityallow; 
    float cooldown;
    float activetime;

    enum AbilityState 
    {
        ready,
        active,
        cooldown
    }

    AbilityState state = AbilityState.ready;
    public KeyCode Key;
    public new string name =  "Sacrificial Lamb Level 1";
    // Update is called once per frame
    void Update()
    {   
        if (ManaSlider.value < ability_two.manaconsume)
        {
            abilityallow = false;
            Debug.Log("NOT Enough Mana for ability_two");
        } else {
            abilityallow = true;
        }

        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(Key) && abilityallow)
                {
                    Activate(playeruse);
                    state = AbilityState.active;
                    activetime = ability_two.activetime;
                    ManaSlider.value -= ability_two.manaconsume;
                    Debug.Log(ability_two.manaconsume);
                } 
                break;
            case AbilityState.active:
                if (activetime > 0)
                {
                    activetime -= Time.deltaTime;
                } else 
                {
                    state = AbilityState.cooldown;
                    cooldown = ability_two.cooldown;
                }
                break;
            case AbilityState.cooldown:
                if (cooldown > 0)
                {
                    cooldown -= Time.deltaTime;
                    if (Input.GetKeyDown(Key))
                    {
                        Debug.Log(ability_two.name + " NOT READY!");
                    }
                } else 
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }

    void Activate(GameObject player) 
    {
        Debug.Log("ability_two used");
    }

}
