using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    HelperScript helper;

    public LayerMask groundLayerMask;

    Rigidbody2D rb;

    float xvel, yvel;

    public Animator anim;

    bool isGrounded;

    private bool isFacingRight = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xvel = 1;

        anim = GetComponent<Animator>();
        helper = gameObject.AddComponent<HelperScript>();
    }

    // Update is called once per frame
    void Update()
    {
        yvel = rb.linearVelocity.y;

        if (xvel < 0)
        {
            if(ExtendedRayCollisionCheck(-0.5f, 0) == false)
            {
                xvel = -xvel;
                helper.FlipObject(false);
            }
        }
        else if (xvel > 0)
        {
            if (ExtendedRayCollisionCheck(0.5f, 0) == false)
            {
                xvel = -xvel;
                helper.FlipObject(true);
            }
        }

        rb.linearVelocity = new Vector2(xvel, yvel);
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
