using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] //Asset Menu
public class AbilityScriptableObject : ScriptableObject
{
    public new string name;
    public float cooldown;
    public float activetime;
    public float manaconsume;
}