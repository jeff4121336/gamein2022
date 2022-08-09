using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashAbility : MonoBehaviour
{

    public GameObject playeruse;
    public AbilityScriptableObject ability_one;
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
    public float dashbasic = 3f;
    public float dashallowance = 0.15f;
    public new string name =  "Dash Level 1";
    protected bool dashdirection;
    Vector2 dash = new Vector2(2f, 0f); 
    // Update is called once per frame
    void Update()
    {   
        if (ManaSlider.value < ability_one.manaconsume)
        {
            abilityallow = false;
            Debug.Log("NOT Enough Mana for ability_one");
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
                    activetime = ability_one.activetime;
                    ManaSlider.value -= ability_one.manaconsume;
                    Debug.Log(ability_one.manaconsume);
                } 
                break;
            case AbilityState.active:
                if (activetime > 0)
                {
                    activetime -= Time.deltaTime;
                } else 
                {
                    state = AbilityState.cooldown;
                    cooldown = ability_one.cooldown;
                }
                break;
            case AbilityState.cooldown:
                if (cooldown > 0)
                {
                    cooldown -= Time.deltaTime;
                    if (Input.GetKeyDown(Key))
                    {
                        Debug.Log(ability_one.name + " NOT READY!");
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
        Object_Detect_Script ODS = player.GetComponent<Object_Detect_Script> ();
        CharacterController2D CC2D = player.GetComponent<CharacterController2D> ();
        Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D> ();

        dashdirection = CC2D.m_FacingRight;
        if (dashdirection) 
        {
            if (ODS.Shortest_RWD <= dashbasic)
            {
                dash = new Vector2(ODS.Shortest_RWD - 0.2f, 0f);
            } else 
            {
                dash = new Vector2(2f, 0f);
            }
            rigidbody2D.position += dash; 
        } else 
        {
           if (ODS.Shortest_LWD <= dashbasic)
            {
                dash = new Vector2(ODS.Shortest_LWD - 0.2f, 0f);
            } else 
            {
                dash = new Vector2(2f, 0f);
            }
            rigidbody2D.position -= dash;
        } 
    }

}
