using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
    ENABLE distance detection for characters with **MonoBehaviour** functions
    Ray detection only, left 3 right 3. For Ground and Celling detection, use rigidbody2D method.
    Modify the script that inherited to this. (Speed/Jump Variables)

    Basic Movement Type 1: 
    Walk/Run/Jump - Charater Controller .Move functions 
    ENABLE rigidbody body dectection // DISABLE ray detection
    use **MonoBehaviour** functions

    Basic Movement Type 2: 
    Dash/Skills - GetKey/User NumPad input functions (.move() need in dash)
    DISABLE rigidbody body dectection // ENABLE ray detection
    use **MonoBehaviour** // **Scriptable Object** functions

    Basic Movement Type 3: 
    Attack/Climb - Another set of scirpts (NO class inheritance)
    use **MonoBehaviour** functions/ GetKey including negation keys

    TYPE 2 apply both unityengine and unityengine.UI, slider implementation needed.
    TYPE 1,3 apply unityengine ONLY.

    Expected Sciprts: 
    1. Movement (2 Sets -> MONO for left/right/jump - 3 scripts + MONO climb/attack - 2 scripts)  
    2. Skills (Scr. Obj. -> Cooldown/Mana/Effects/Animation/Responses/Multi-Object interaction)
    (Movement related skills -> inherited to XXXXMovement.cs)
    3. Enemies ... 
*/
public class Object_Detect_Script : MonoBehaviour
{

    protected CapsuleCollider2D bc2d;
    protected ContactFilter2D contactfilter;
    protected RaycastHit2D[] hitbuffer = new RaycastHit2D[16];
    protected float bcsizeX; // BOXCOLLIDER
    protected float bcsizeY;
    protected float leftwalldistance; // 6 Rays to wall
    protected float rightwalldistance;
    protected float leftwalldistanceUP;
    protected float rightwalldistanceUP;
    protected float leftwalldistanceDOWN;
    protected float rightwalldistanceDOWN;
    public float Shortest_LWD; // For Dash
    public float Shortest_RWD;
    RaycastHit2D Lefthit; // 6 Rays 
    RaycastHit2D Righthit;
    RaycastHit2D LefthitUP;
    RaycastHit2D RighthitUP;
    RaycastHit2D LefthitDOWN;
    RaycastHit2D RighthitDOWN;

    
    void OnEnable() 
    {
    //  rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<CapsuleCollider2D>();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        contactfilter.useTriggers = false;
        contactfilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactfilter.useLayerMask = true;
        bcsizeX = bc2d.size.x;
        bcsizeY = bc2d.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 ShiftedLeftrayUP = new Vector2(transform.position.x, transform.position.y + bc2d.size.y / 2 );
        Vector2 ShiftedLeftrayDOWN = new Vector2(transform.position.x, transform.position.y - bc2d.size.y / 2);
        Vector2 ShiftedRightrayUP = new Vector2(transform.position.x, transform.position.y + bc2d.size.y / 2);
        Vector2 ShiftedRightrayDOWN = new Vector2(transform.position.x, transform.position.y - bc2d.size.y / 2);

        Lefthit = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, 1 << LayerMask.NameToLayer("FloorWall"));
        Righthit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("FloorWall"));
        LefthitUP = Physics2D.Raycast(ShiftedLeftrayUP, Vector2.left, Mathf.Infinity, 1 << LayerMask.NameToLayer("FloorWall"));
        RighthitUP = Physics2D.Raycast(ShiftedRightrayUP, Vector2.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("FloorWall"));
        LefthitDOWN = Physics2D.Raycast(ShiftedLeftrayDOWN, Vector2.left, Mathf.Infinity, 1 << LayerMask.NameToLayer("FloorWall"));
        RighthitDOWN = Physics2D.Raycast(ShiftedRightrayDOWN, Vector2.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("FloorWall"));

        leftwalldistance = Lefthit.distance - bcsizeX / 2;
        rightwalldistance = Righthit.distance - bcsizeY / 2;
        leftwalldistanceUP = LefthitUP.distance - bcsizeX / 2;
        rightwalldistanceUP = RighthitUP.distance - bcsizeY / 2;
        leftwalldistanceDOWN = LefthitDOWN.distance - bcsizeX / 2;
        rightwalldistanceDOWN = RighthitDOWN.distance - bcsizeY / 2;

        Debug.DrawRay(transform.position, Vector2.right * rightwalldistance, Color.yellow); 
        Debug.DrawRay(transform.position, Vector2.left * leftwalldistance, Color.blue);
        Debug.DrawRay(ShiftedRightrayUP, Vector2.right * rightwalldistanceUP, Color.yellow); 
        Debug.DrawRay(ShiftedLeftrayUP, Vector2.left * leftwalldistanceUP, Color.blue);
        Debug.DrawRay(ShiftedRightrayDOWN, Vector2.right * rightwalldistanceDOWN, Color.yellow); 
        Debug.DrawRay(ShiftedLeftrayDOWN, Vector2.left * leftwalldistanceDOWN, Color.blue);

        Shortest_LWD = Mathf.Min(leftwalldistanceUP, leftwalldistance, leftwalldistanceDOWN);
        Shortest_RWD = Mathf.Min(rightwalldistanceUP, rightwalldistance, rightwalldistanceDOWN);

    }

}