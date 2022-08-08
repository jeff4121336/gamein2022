using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamage : MonoBehaviour
{
    [SerializeField] private float timetodestory;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timetodestory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
