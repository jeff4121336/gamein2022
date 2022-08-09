using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaText : MonoBehaviour
{
    public LinailHealthMana LHM;
    public Text mana_text;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = LHM.default_mana;
    }

    // Update is called once per frame
    void Update()
    {
        mana_text.text = Mathf.Round(LHM.Mana.value).ToString();
    }
}
