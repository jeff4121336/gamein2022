using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{    
    public GameObject gameobject;
    public BoxCollider2D boundsbox;
    public float cameramovespeed;
    private Vector3 targetpos;
    private Vector3 minbounds;
    private Vector3 maxbounds;
    
    private Camera thecamera;
    private float halfheight;
    private float halfwidth;

    void Start() {
        
        minbounds = boundsbox.bounds.min;
        maxbounds = boundsbox.bounds.max;
        thecamera = GetComponent<Camera> ();
        halfheight = thecamera.orthographicSize;
        halfwidth = halfheight * Screen.width / Screen.height;
        transform.position = new Vector3(gameobject.transform.position.x, gameobject.transform.position.y, transform.position.z - 10f);
    }
     
    void Update() {
        targetpos = new Vector3(gameobject.transform.position.x, gameobject.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetpos, cameramovespeed * Time.deltaTime);

    
        float clampedX = Mathf.Clamp (transform.position.x, minbounds.x + halfwidth, maxbounds.x - halfwidth);
        float clampedY = Mathf.Clamp (transform.position.y, minbounds.y + halfheight, maxbounds.y - halfheight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

}
