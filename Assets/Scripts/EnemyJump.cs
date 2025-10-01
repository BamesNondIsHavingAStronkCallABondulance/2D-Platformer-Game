using UnityEngine;

public class EnemyJump : MonoBehaviour
{

    public Player playerScript;
    public LayerMask groundLayerMask;
    public Animator anim;

    Rigidbody2D rb;

    float xvel, yvel;
    float delay;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        yvel = 0;
        delay = 2;
    }

    // Update is called once per frame
    void Update()
    {
        JumpCheck();

        //print("Player has " + playerScript.health + " health.");
    }

    void JumpCheck()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            if(ExtendedRayCollisionCheck(0.5f, 0) == false)
            yvel = 5;
            delay = 3;
            rb.linearVelocity = new Vector2(xvel, yvel);
        }

    }
    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.1f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength, groundLayerMask);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, Vector3.down * rayLength, hitColor);

        return hitSomething;

    }
}
