using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image image;
    public float time;
    public Color flash_color;
    private Color default_color;
    // Start is called before the first frame update
    void Start()
    {
        default_color = image.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void screenflash()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash() 
    {
        image.color = flash_color;
        yield return new WaitForSeconds(time);
        image.color = default_color;
    }
}
