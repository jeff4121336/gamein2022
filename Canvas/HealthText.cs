using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public LinailHealthMana LHM;
    public Text health_text;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = LHM.default_health;
    }

    // Update is called once per frame
    void Update()
    {
        health_text.text = Mathf.Round(LHM.Health.value).ToString();
    }

}
